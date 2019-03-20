using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cosmonaut;
using Cosmonaut.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using SplunkTagManager.Models;


namespace SplunkTagManager.Indices
{
    [Route("api/[Controller]")]
    public class IndicesController : Controller
    {
        private readonly ICosmosStore<Index> _indexCosmosStore;

        public IndicesController(ICosmosStore<Index> indexCosmosStore)
        {
            _indexCosmosStore = indexCosmosStore;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string continuationToken, CancellationToken cancellationToken, [FromQuery] int pageSize = 10)
        {
            if (pageSize > 50 || pageSize <= 0)
            {
                return BadRequest("Page Size Invalid! Must be 0 < pageSize < 50");
            }

            FeedOptions options = null;
            if (continuationToken == null || continuationToken.Trim().Length == 0)
            {
                options = new FeedOptions { RequestContinuation = continuationToken, MaxItemCount = pageSize };
            }

            var indices = await _indexCosmosStore.Query(options).ToListAsync(cancellationToken);

            return Ok(indices.OrderBy(index => index.Name));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid index Id");
            }

            var index = await _indexCosmosStore.FindAsync(id.ToString());
            if (index == null)
            {
                return NotFound();
            }

            var response = new GetIndexResponse
            {
                Identifier = index.Id,
                Name = index.Name,
                Location = index.Location,
                Enabled = index.Enabled
            };

            return Ok(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(
            [FromBody] CreateIndexRequest request, CancellationToken cancellationToken)
        {
            var indexToCreate = new Index
            {
                Name = request.Name,
                Location = request.Location
            };

            var result = await _indexCosmosStore.UpsertAsync(indexToCreate);

            var indexId = result.Entity.Id;

            if (indexId == null)
            {
                return BadRequest();
            }

            return Ok(indexId);
        }
        
        // TODO: change to HTTP PATCH
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] EditIndexRequest request, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid index Id");
            }

            if (request.Name.Trim().Length == 0)
            {
                return BadRequest("Index Name can not be empty");
            }

            var existingIndex = await _indexCosmosStore.FindAsync(id.ToString());

            if (existingIndex == null)
            {
                return NotFound();
            }

            var indexToSave = new Index
            {
                Id = id.ToString(),
                Name = request.Name,
                Location = request.Location,
                Enabled = request.Enabled
            };

            var result = await _indexCosmosStore.UpdateAsync(indexToSave);

            var indexId = result.Entity.Id;

            if (indexId == null)
            {
                return BadRequest();
            }

            return Ok(indexId);
        }
        
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _indexCosmosStore.RemoveByIdAsync(id.ToString());

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Upsert(
            [FromBody] UpsertIndexRequest request, CancellationToken cancellationToken)
        {
            var indexToUpsert = new Index
            {
                Id = request.Id,
                Name = request.Name,
                Location = request.Location,
                Enabled = request.Enabled
            };

            var result = await _indexCosmosStore.UpsertAsync(indexToUpsert);

            var indexId = result.Entity.Id;

            if (indexId == null)
            {
                return BadRequest();
            }

            return Ok(indexId);
        }
        
    }
}