namespace SplunkTagManager.Indices
{
    public class CreateIndexRequest
    {
        public string Name { get; set; }
        
        public string Location { get; set; }
        
        public bool Enabled { get; set; }
        
    }
}