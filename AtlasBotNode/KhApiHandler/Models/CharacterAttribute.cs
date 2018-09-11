using Newtonsoft.Json;
using System.Collections.Generic;

namespace KhApiHandler.Models
{
    public class CharacterAttribute
    {
        public List<MovementAttribute> Attributes { get; }

        public CharacterAttribute(List<MovementAttribute> attributes)
        {
            this.Attributes = attributes;
        }

        public CharacterAttribute()
        {
        }
    }

    public class MovementAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ownerId")]
        public int OwnerId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}