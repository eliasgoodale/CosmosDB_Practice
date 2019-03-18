using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Cosmonaut;
using Cosmonaut.Attributes;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace QueryTest
{
    [CosmosCollection("indexTagAdminItems")]
    public class Index : ISharedCosmosEntity {
        public string CosmosEntityName { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Location { get; set; }

        public bool Enabled { get; set; }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            const string EndpointUrl = @"";
            const string PrimaryKey =
                @"";
            DocumentClient client;
            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            var queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<Index> familyQuery = client.CreateDocumentQuery<Index>(
                UriFactory.CreateDocumentCollectionUri("index_tag_admin_db", "indexTagAdminItems"));
            foreach (var index in familyQuery)
            {
                Console.WriteLine("\tRead {0}", index.Id + " " + index.Name);
            }

            Console.Read();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}