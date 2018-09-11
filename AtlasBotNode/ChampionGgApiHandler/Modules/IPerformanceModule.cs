using ChampionGgApiHandler.Exceptions;
using ChampionGgApiHandler.Models.Performance;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChampionGgApiHandler.Modules
{
    public interface IPerformanceModule
    {
        Task<Performance> GetDefaultPerformance();
    }

    public class PerformanceModule : IPerformanceModule
    {
        private readonly Uri _baseUri;

        public PerformanceModule(Uri uri)
        {
            _baseUri = uri;
        }

        public async Task<Performance> GetDefaultPerformance()
        {
            var client = new RestClient(_baseUri);
            var request = new RestRequest($"overall?api_key={KeyStorage.ApiKey}", Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
                throw new ServiceNotAvailableException();
            var returnObject = JsonConvert.DeserializeObject<List<Performance>>(response.Content);

            return returnObject.Any() ? returnObject[0] : null;
        }
    }
}