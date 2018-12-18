using System.Collections.Generic;
using Newtonsoft.Json;

namespace AtlasBotNode.Configuration.Models
{
    public class BaseConfiguration
    {
        [JsonProperty(PropertyName = "keys")]
        public KeyConfiguration KeyConfiguration { get; set; }
        
        [JsonProperty(PropertyName = "modules")]
        public List<string> Modules { get; set; }
        
        [JsonProperty(PropertyName = "commander")]
        public CommanderConfiguration CommanderConfiguration { get; set; }
    }
}