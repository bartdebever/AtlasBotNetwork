using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChampionGgApiHandler.Exceptions;
using ChampionGgApiHandler.Models.Champion;
using Newtonsoft.Json;
using RestSharp;

namespace ChampionGgApiHandler.Modules
{
    public interface IChampionModule
    {
        Task<ChampionData> GetChampionStatsAsync(int championId);
    }

    public class ChampionModule : IChampionModule
    {
        private readonly Uri _baseUri;

        public ChampionModule(Uri uri)
        {
            _baseUri = uri;
        }

        public async Task<ChampionData> GetChampionStatsAsync(int championId)
        {
            var client = new RestClient(_baseUri);
            var request =
                new RestRequest(
                    $"champions/{championId}?limit=1&champData=kda,damage,positions,finalitems,hashes&api_key={KeyStorage.ApiKey}");
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
                throw new ServiceNotAvailableException();

            return JsonConvert.DeserializeObject<List<ChampionData>>(response.Content).FirstOrDefault();
        }
    }
}