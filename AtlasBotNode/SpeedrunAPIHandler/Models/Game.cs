using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpeedrunAPIHandler.Models
{
    public class RunGame
    {
        [JsonProperty("data")]
        public Game Game { get; set; }
    }

    public class Game
    {
        [JsonProperty("names")]
        public Dictionary<string, string> Names { get; set; }

        [JsonProperty("assets")]
        public Dictionary<string, Image> Assets { get; set; }
    }
}