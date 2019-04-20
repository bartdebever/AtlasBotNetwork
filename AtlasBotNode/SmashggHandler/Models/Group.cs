using Newtonsoft.Json;

namespace SmashggHandler.Models
{
    public class Group
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("phaseId")] public int PhaseId { get; set; }
    }
}