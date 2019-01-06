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
        /// <summary>
        /// Gets the best performing champions per lane as provided by Champion.gg.
        /// </summary>
        /// <returns>An awaitable task with the <see cref="Performance"/> object as result.</returns>
        /// <exception cref="ServiceNotAvailableException">Thrown when the response is unsuccessful or has no content.</exception>
        Task<Performance> GetDefaultPerformanceAsync();
    }

    public class PerformanceModule : IPerformanceModule
    {
        private readonly Uri _baseUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceModule"/> class.
        /// </summary>
        /// <param name="uri">The base url that should be used in requests.</param>
        public PerformanceModule(Uri uri)
        {
            _baseUri = uri;
        }

        /// <inheritdoc />
        public async Task<Performance> GetDefaultPerformanceAsync()
        {
            var client = new RestClient(_baseUri);
            var request = new RestRequest($"overall?api_key={KeyStorage.ApiKey}", Method.GET);
            var response = await client.ExecuteTaskAsync(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            {
                throw new ServiceNotAvailableException();
            }

            var returnObject = JsonConvert.DeserializeObject<List<Performance>>(response.Content);

            return returnObject.FirstOrDefault();
        }
    }
}