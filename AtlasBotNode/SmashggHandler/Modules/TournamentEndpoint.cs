using Newtonsoft.Json;
using RestSharp;
using SmashggHandler.Exceptions;
using SmashggHandler.Models;
using SmashggHandler.Modules.Interfaces;
using System.Threading.Tasks;

namespace SmashggHandler.Modules
{
    public class TournamentEndpoint : ITournamentEndpoint
    {
        private const string Uri = "http://api.smash.gg/";
        private const string TournamentEndpointUrl = "tournament/";
        private const string UpcomingTournaments = "/public/tournaments/schedule?per_page=5&filter={\"upcoming\"%3Atrue%2C\"videogameIds\"%3A\"\"%2C\"attendeeCount\"%3A[\"gt%3A200%2Clte%3A500\"%2C\"gt%3A500%2Clte%3A1000\"%2C\"gt%3A1000\"%2C\"gt%3A100%2Clte%3A200\"]}&page=1";

        public async Task<TournamentRoot> GetTournamentByName(string name)
        {
            var client = new RestClient(Uri);
            var request = new RestRequest($"{TournamentEndpointUrl}{name}?expand[]=event", Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
                throw new TournamentNotFoundException();
            return JsonConvert.DeserializeObject<TournamentRoot>(response.Content);
        }

        public async Task<TournamentScheduleRoot> GetUpcomingTournaments()
        {
            var client = new RestClient(Uri);
            var request = new RestRequest($"{UpcomingTournaments}", Method.GET);
            var response = await client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
                throw new TournamentNotFoundException();
            return JsonConvert.DeserializeObject<TournamentScheduleRoot>(response.Content);
        }
    }
}