using System;
using Cosmonaut;
using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace SplunkTagManager.Models
{
    [CosmosCollection("indexTagAdminItems")]
    public class Tag : ISharedCosmosEntity {
        public string CosmosEntityName { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Location { get; set; }
        
        public string IndexId { get; set; }
        
        public bool Enabled { get; set; }
        
        public DateTime ModifiedDate { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}