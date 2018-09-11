using Newtonsoft.Json;

namespace SmashggHandler.Models
{
    public class Phase
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("eventId")]
        public int? EventId { get; set; }

        [JsonProperty("groupCount")]
        public int? GroupCount { get; set; }
    }
}