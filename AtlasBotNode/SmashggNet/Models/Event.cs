using System.Collections.Generic;
using Newtonsoft.Json;
using SmashggNet.Models.SubObjects;

namespace SmashggNet.Models
{
    public class Event
    {
        /// <summary>
        ///     Gets or sets the name of the event.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the page of standings for this event.
        ///     <para>Use <see cref="Standings" /> to get the internal standings instead.</para>
        /// </summary>
        [JsonProperty("standings")]
        public StandingsPage Page { get; set; }

        /// <summary>
        ///     Gets the standings from the <see cref="Page" /> object.
        ///     <para>See this as a shortcut.</para>
        /// </summary>
        [JsonIgnore]
        public List<Standing> Standings => Page.Standings;
    }
}