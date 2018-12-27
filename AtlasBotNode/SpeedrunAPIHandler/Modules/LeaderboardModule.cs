using Newtonsoft.Json;
using RestSharp;
using SpeedrunAPIHandler.Models;
using System;
using System.Threading.Tasks;
using SpeedrunAPIHandler.Models.Leaderboards;

namespace SpeedrunAPIHandler.Modules
{
    public interface ILeaderboardModule
    {
        Task<Leaderboard> GetLeaderboard(string game, string category, int players = 10);
    }

    public class LeaderboardModule : ILeaderboardModule
    {
        public async Task<Leaderboard> GetLeaderboard(string game, string category, int players = 10)
        {
            var client = new RestClient(new Uri("http://speedrun.com"));
            var request = new RestRequest($"api/v1/leaderboards/{game}/category/{category}?embed=players,game,category&top={players}", Method.GET);
            request.AddHeader("X-API-Key", SpeedrunAPIClient.ApiKey);

            var response = await client.ExecuteTaskAsync(request);
            var root = JsonConvert.DeserializeObject<RootLeaderboard>(response.Content);
            return root?.Leaderboard;
        }
    }
}