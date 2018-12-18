using Newtonsoft.Json;

namespace AtlasBotNode.Configuration.Models
{
    public class CommanderConfiguration
    {
        [JsonProperty("use-commander")]
        public bool UseCommander { get; set; }
        
        [JsonProperty("commander-ip")]
        public string IpAddress { get; set; }
        
        [JsonProperty("commander-port")]
        public int Port { get; set; }
        
        [JsonProperty("node-name")]
        public string NodeName { get; set; }
        
        [JsonProperty("commander-token")]
        public string CommanderToken { get; set; }
    }
}