using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using SmashggNet;

namespace AtlasBotNode.Modules.Smash
{
    [Group("sgg")]
    public class SmashggNewModule : ModuleBase
    {
        [Command("standings")]
        public async Task GetTournamentStandings(string tournament)
        {
            var client = new SmashggNewClient();
            var tournamentObject = await client.Tournament.GetTournamentStandings(tournament);

            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle(tournamentObject.Name);
            foreach(var smashEvent in tournamentObject.Event)
            {
                var stringBuilder = new StringBuilder();
                foreach (var standing in smashEvent.Standings)
                {
                    stringBuilder.AppendLine($"{standing.Entrant.Name}: {standing.Ranking}");
                }

                embedBuilder.AddField(smashEvent.Name, stringBuilder.ToString());
            }

            await ReplyAsync(string.Empty, embed: embedBuilder.Build());
        }
    }
}
