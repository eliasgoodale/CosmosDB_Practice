namespace SplunkTagManager.Indices
{
    public class UpsertIndexRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Enabled { get; set; }   
    }
}