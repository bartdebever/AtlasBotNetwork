using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmashggHandler.Models
{
    public class TournamentScheduleRoot
    {
        [JsonProperty("items")] public TournamentScheduleItem Items { get; set; }
    }

    public class TournamentScheduleItem
    {
        [JsonProperty("entities")] public TournamentScheduleEntity Entities { get; set; }
    }

    public class TournamentScheduleEntity
    {
        [JsonProperty("tournament")] public List<Tournament> Tournament { get; set; }

        [JsonProperty("event")] public List<Event> Events { get; set; }

        [JsonProperty("videogame")] public List<Videogame> Videogames { get; set; }
    }
}