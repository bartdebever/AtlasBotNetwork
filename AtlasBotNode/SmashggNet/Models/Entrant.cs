using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace SmashggNet.Models
{
    public class Entrant
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
