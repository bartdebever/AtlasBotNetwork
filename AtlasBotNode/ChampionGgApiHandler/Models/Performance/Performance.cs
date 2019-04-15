using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChampionGgApiHandler.Models.Performance
{
    public class PerformanceRoot
    {
        [JsonProperty("data")] public List<Performance> Data { get; set; }
    }

    public class Performance
    {
        [JsonProperty("elo")] public string Elo { get; set; }

        [JsonProperty("patch")] public string Patch { get; set; }

        [JsonProperty("positions")] public Dictionary<string, RoleStatistic> RoleStatistics { get; set; }
    }
}