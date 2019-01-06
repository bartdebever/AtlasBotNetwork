using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Games
{
    public class Game
    {
        /// <summary>
        /// Gets or sets a dictionary of the language and the name of the game in that language.
        /// </summary>
        [JsonProperty("names")]
        public Dictionary<string, string> Names { get; set; }

        /// <summary>
        /// Gets or sets a dictionary of the image type and the image object.
        /// </summary>
        [JsonProperty("assets")]
        public Dictionary<string, Image> Assets { get; set; }
    }
}