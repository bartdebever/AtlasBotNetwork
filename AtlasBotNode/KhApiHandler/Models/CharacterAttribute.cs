using System.Collections.Generic;
using Newtonsoft.Json;

namespace KhApiHandler.Models
{
    public class CharacterAttribute
    {
        public CharacterAttribute(List<MovementAttribute> attributes)
        {
            Attributes = attributes;
        }

        public CharacterAttribute()
        {
        }

        public List<MovementAttribute> Attributes { get; }
    }

    public class MovementAttribute
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("ownerId")] public int OwnerId { get; set; }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("value")] public string Value { get; set; }
    }
}