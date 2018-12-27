namespace AtlasBotNode.Configuration.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A class used to contain the configuration of AtlasBot.
    /// </summary>
    public class BaseConfiguration
    {
        /// <summary>
        /// Gets or sets the key section of the configuration.
        /// </summary>
        [JsonProperty(PropertyName = "keys")]
        public KeyConfiguration KeyConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the loaded modules.
        /// </summary>
        [JsonProperty(PropertyName = "modules")]
        public List<string> Modules { get; set; }

        /// <summary>
        /// Gets or sets the commander settings for this node.
        /// </summary>
        [JsonProperty(PropertyName = "commander")]
        public CommanderConfiguration CommanderConfiguration { get; set; }
    }
}