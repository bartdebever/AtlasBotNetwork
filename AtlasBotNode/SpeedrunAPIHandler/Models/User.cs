using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpeedrunAPIHandler.Models
{
    public class User
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("names")] public Dictionary<string, string> Names { get; set; }
    }
}