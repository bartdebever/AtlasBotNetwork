using Newtonsoft.Json;
using SpeedrunAPIHandler.Models.Runs;

namespace SpeedrunAPIHandler.Models.Leaderboards
{
    public class RunList
    {
        [JsonProperty("place")] public int Place { get; set; }

        [JsonProperty("run")] public Run Run { get; set; }
    }
}