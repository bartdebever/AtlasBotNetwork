using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LoLHandler.Dtos
{
    public class ChampionRoot
    {
        [JsonProperty("data")]
        public Dictionary<string, ChampionDto> Champions { get; set; }
    }
}
