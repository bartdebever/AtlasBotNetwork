namespace SmashggNet.Models.SubObjects
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class StandingsPage
    {
        /// <summary>
        /// Gets or sets the standings of the event. These can be current or finished.
        /// </summary>
        [JsonProperty("nodes")]
        public List<Standing> Standings { get; set; }
    }
}
