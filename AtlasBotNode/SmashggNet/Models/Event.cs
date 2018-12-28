namespace SmashggNet.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using SubObjects;

    public class Event
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("standings")]
        public StandingsPage Page { get; set; }

        [JsonIgnore]
        public List<Standing> Standings => Page.Standings;
    }
}