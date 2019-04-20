using System.Collections.Generic;
using Newtonsoft.Json;

namespace LoLHandler.Dtos
{
    public class ChampionDto
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("lore")] public string Lore { get; set; }

        [JsonProperty("tags")] public List<string> Tags { get; set; }

        /// <summary>
        ///     Details "attack", "defense", "magic" and "difficulty"
        /// </summary>
        [JsonProperty("info")]
        public Dictionary<string, int?> Info { get; set; }

        [JsonProperty("spells")] public List<SpellDto> Spells { get; set; }

        [JsonProperty("passive")] public PassiveDto Passive { get; set; }
    }
}