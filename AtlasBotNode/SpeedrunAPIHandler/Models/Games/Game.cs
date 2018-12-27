using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Games
{
    public class Game
    {
        [JsonProperty("names")]
        public Dictionary<string, string> Names { get; set; }

        [JsonProperty("assets")]
        public Dictionary<string, Image> Assets { get; set; }
    }
}