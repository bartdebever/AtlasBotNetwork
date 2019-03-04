using KhApiHandler.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace KhApiHandler.Modules
{
    public interface ICharacterModule
    {
        Task<Character> GetCharacterByNameAsync(string name);
    }

    public class CharacterModule : ICharacterModule
    {
        public async Task<Character> GetCharacterByNameAsync(string name)
        {
            var client = new RestClient("http://beta-api-kuroganehammer.azurewebsites.net");
            var request = new RestRequest($"api/characters/name/{name}");
            var response = await client.ExecuteTaskAsync(request);

            return JsonConvert.DeserializeObject<Character>(response.Content);
        }
    }
}