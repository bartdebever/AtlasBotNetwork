using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpeedrunAPIHandler.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("names")]
        public Dictionary<string, string> Names { get; set; }
    }
}