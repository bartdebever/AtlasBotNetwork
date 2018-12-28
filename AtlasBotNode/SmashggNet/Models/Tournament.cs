using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace SmashggNet.Models
{
    public class Tournament
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("events")]
        public List<Event> Event { get; set; }
    }
}
