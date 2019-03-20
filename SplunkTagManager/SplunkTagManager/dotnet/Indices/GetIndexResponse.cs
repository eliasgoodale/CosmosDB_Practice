namespace SplunkTagManager.Indices
{
    public class GetIndexResponse
    {
        public string Identifier { get; set; }
        
        public string Name { get; set; }
        
        public string Location { get; set; }
        
        public bool Enabled { get; set; }
    }
}