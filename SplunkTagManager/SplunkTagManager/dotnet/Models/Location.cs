using Cosmonaut;
using Cosmonaut.Attributes;

namespace SplunkTagManager.Models
{
    [CosmosCollection("indexTagAdminItems")]
    public class Location : ISharedCosmosEntity 
    {
        public string CosmosEntityName { get; set; }
        public string Region { get; set; } //e.g. ND, NM, TX, etc
        public string Name { get; set; } //should be uniqueId
    }
}