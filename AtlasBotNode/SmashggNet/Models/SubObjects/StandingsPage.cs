namespace SmashggNet.Models.SubObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class StandingsPage
    {
        [JsonProperty("nodes")]
        public List<Standing> Standings { get; set; }
    }
}
