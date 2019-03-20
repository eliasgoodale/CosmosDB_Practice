namespace SplunkTagManager.Indices
{
    public class EditIndexRequest
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Enabled { get; set; }
    }
}