using Newtonsoft.Json;

namespace AtlasBotNode.Configuration.Models
{
    public class CommanderConfiguration
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the commander should be used.
        /// </summary>
        [JsonProperty("use-commander")]
        public bool UseCommander { get; set; }

        /// <summary>
        ///     Gets or sets the value of the IP Address
        /// </summary>
        [JsonProperty("commander-ip")]
        public string IpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the port for the commander.
        /// </summary>
        [JsonProperty("commander-port")]
        public int Port { get; set; }

        /// <summary>
        ///     Gets or sets the name of the node needing to be passed to the commander.
        /// </summary>
        [JsonProperty("node-name")]
        public string NodeName { get; set; }

        /// <summary>
        ///     Gets or sets the token used to authenticate with the commander.
        /// </summary>
        [JsonProperty("commander-token")]
        public string CommanderToken { get; set; }
    }
}