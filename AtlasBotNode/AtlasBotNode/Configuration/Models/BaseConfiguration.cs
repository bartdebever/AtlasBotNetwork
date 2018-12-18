using System.Collections.Generic;

namespace AtlasBotNode.Configuration.Models
{
    public class BaseConfiguration
    {
        public KeyConfiguration KeyConfiguration { get; set; }
        
        public List<string> Modules { get; set; }
        
        public CommanderConfiguration CommanderConfiguration { get; set; }
    }
}