using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LoLHandler.Dtos
{
    public class SpellDto
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        public string GetKey(string name) => Id.Replace(name, " ");
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }
        
        [JsonProperty("cooldownBurn")]
        public string CooldownBurn { get; set; }
    }
}
