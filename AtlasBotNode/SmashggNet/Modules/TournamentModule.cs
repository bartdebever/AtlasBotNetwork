using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using SmashggNet.Helpers;
using SmashggNet.Models;
using SmashggNet.Modules.Interfaces;

namespace SmashggNet.Modules
{
    public class TournamentModule : ITournamentModule
    {
        /// <inheritdoc />
        public async Task<Tournament> GetTournamentStandings(string tournament)
        {
            var request = new GraphQLRequest
            {
                Query =
                    "query TournamentQuery($slug: String) \r\n{\r\n\t\ttournament(slug: $slug){\r\n\t\t\tname\r\n\t\t\tevents {\r\n\t\t\t\tname\r\n        standings(query: {\r\n      perPage: 10\r\n      page: 1\r\n    }){\r\n      nodes {\r\n        standing\r\n        entrant {\r\n          name\r\n        }\r\n\t\t\t}\r\n\t\t}\r\n      }\r\n  }\r\n}",
                OperationName = "TournamentQuery",
                Variables = new {slug = tournament}
            };

            using (var graphQlClient = new GraphQLClient(SmashggConstants.BaseUrl))
            {
                graphQlClient.DefaultRequestHeaders.Add(
                    "Authorization",
                    $"Bearer {SmashggClient.ApiToken}");

                try
                {
                    var graphQlResponse = await graphQlClient.PostAsync(request);
                    return graphQlResponse.GetDataFieldAs<Tournament>("tournament");
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}