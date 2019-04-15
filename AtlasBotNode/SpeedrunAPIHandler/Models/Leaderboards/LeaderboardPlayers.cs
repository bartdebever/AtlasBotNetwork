using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models.Leaderboards
{
    public class LeaderboardPlayers
    {
        [JsonProperty("data")] public List<User> Players { get; set; }
    }
}