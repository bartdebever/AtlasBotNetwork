using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpeedrunAPIHandler.Models
{
    public class Run
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("weblink")]
        public string WebLink { get; set; }

        [JsonProperty("times")]
        public Times Time { get; set; }

        [JsonProperty("players")]
        public List<RunPlayer> Players { get; set; }
    }

    public class Times
    {
        [JsonProperty("primary")]
        public string Primary { get; set; }
    }

    public class RunPlayer
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}