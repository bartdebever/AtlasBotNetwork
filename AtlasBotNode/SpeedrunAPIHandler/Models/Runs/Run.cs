using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Runs
{
    public class Run
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("weblink")] public string WebLink { get; set; }

        [JsonProperty("times")] public Times Time { get; set; }

        [JsonProperty("players")] public List<RunPlayer> Players { get; set; }
    }
}