using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cosmonaut;
using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace SplunkTagManager.Models
{
    [CosmosCollection("indexTagAdminItems")]
    public class Index : ISharedCosmosEntity
    {
        public string CosmosEntityName { get; set; }
        
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
        
        public string Name { get; set; }
        
        public string Location { get; set; }
        
        public string Status { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime LastRunDate { get; set; }
        
        public string RunStatus { get; set; }
        
        public string[] Tags { get; set; }
        
    }
}