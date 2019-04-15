using Newtonsoft.Json;

namespace SmashggHandler.Models
{
    public class Event
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("tournamentId")] public int TournamentId { get; set; }

        [JsonProperty("videogameId")] public int VideogameId { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}