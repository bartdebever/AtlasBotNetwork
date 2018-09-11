using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmashggHandler.Models
{
    public class TournamentRoot
    {
        [JsonProperty("entities")]
        public TournamentEntities Entities { get; set; }
    }

    public class TournamentEntities
    {
        [JsonProperty("tournament")]
        public Tournament Tournament { get; set; }

        [JsonProperty("event")]
        public List<Event> Events { get; set; }

        [JsonProperty("videogame")]
        public List<Videogame> Videogames { get; set; }

        [JsonProperty("phase")]
        public List<Phase> Phases { get; set; }

        [JsonProperty("group")]
        public List<Group> Groups { get; set; }
    }
}