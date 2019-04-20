using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmashggHandler.Models
{
    public class Tournament
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("seriesId")] public int? SeriesId { get; set; }

        [JsonProperty("ownerId")] public int? OwnerId { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("slug")] public string Slug { get; set; }

        public string Url => $"https://www.smash.gg/tournament/{Slug}";

        [JsonProperty("timezone")] public string Timezone { get; set; }

        [JsonProperty("private")] public bool? Private { get; set; }

        [JsonProperty("startAt")] public long StartAt { get; set; }

        [JsonProperty("endAt")] public long EndAt { get; set; }

        public DateTime StartDate => new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(StartAt);
        public DateTime EndDate => new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(EndAt);

        [JsonProperty("venueName")] public string VenueName { get; set; }

        [JsonProperty("venueAddress")] public string VenueAddress { get; set; }

        [JsonProperty("images")] public List<SmashggImage> Images { get; set; }
    }
}