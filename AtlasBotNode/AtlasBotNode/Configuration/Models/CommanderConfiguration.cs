namespace AtlasBotNode.Configuration.Models
{
    public class CommanderConfiguration
    {
        public bool UseCommander { get; set; }
        
        public string IpAddress { get; set; }
        
        public int Port { get; set; }
        
        public string NodeName { get; set; }
        
        public string CommanderToken { get; set; }
    }
}