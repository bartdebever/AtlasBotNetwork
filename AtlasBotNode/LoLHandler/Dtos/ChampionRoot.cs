using System.Collections.Generic;
using Newtonsoft.Json;

namespace LoLHandler.Dtos
{
    public class ChampionRoot
    {
        [JsonProperty("data")] public Dictionary<string, ChampionDto> Champions { get; set; }
    }
}