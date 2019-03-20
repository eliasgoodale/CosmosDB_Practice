using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmonaut;
using Cosmonaut.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SplunkTagManager.Models;

namespace SplunkTagManager
{
    public class LookupService
    {
        private readonly IServiceProvider _sp;

        public LookupService(IServiceProvider sp)
        {
            _sp = sp;
        }

        public Task<List<Location>> GetLocations()
        {
            var locationRepo = _sp.GetService<ICosmosStore<Location>>();
            return locationRepo.Query().ToListAsync();
        }

        public Task<List<Index>> GetIndexesByLocation(string location)
        {
            var locationRepo = _sp.GetService<ICosmosStore<Index>>();
            return locationRepo.Query().Where(index => index.Location == location).ToListAsync();
        }

        public Task<List<Tag>> GetTagsByIndex(string indexId)
        {
            var tagRepo = _sp.GetService<ICosmosStore<Tag>>();
            return tagRepo.Query().Where(tag => tag.IndexId == indexId).ToListAsync();
        }
    }
}