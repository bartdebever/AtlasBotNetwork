using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private const int MAX_STANDINGS = 3;
        [Command("standings")]
        [Description("Gets the current standings or final results for a tournament.")]
        public async Task GetTournamentStandings(string tournament)
        {
            var tournamentObject = await SmashggClient.Tournament.GetTournamentStandings(tournament);

            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle(tournamentObject.Name);

            var amountOfStandings = tournamentObject.Event.Count;

            if (amountOfStandings > MAX_STANDINGS)
            {
                await ReplyAsync("Too many results!");
                return;
            }

            for (var i = 0; i < amountOfStandings; i++)
            {
                var smashEvent = tournamentObject.Event[i];
                var stringBuilder = new StringBuilder();
                foreach (var standing in smashEvent.Standings)
                {
                    stringBuilder.AppendLine($"**{standing.Entrant.Name}**: {standing.Ranking}");
                }

                embedBuilder.AddField(smashEvent.Name, stringBuilder.ToString(), true);
            }

            await ReplyAsync(string.Empty, embed: embedBuilder.Build());
        }

        [Command("standings")]
        public async Task GetTournamentStandings(string eventName, [Remainder] string tournament)
        {
            var tournamentObject = await SmashggClient.Tournament.GetTournamentStandings(tournament);

            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle(tournamentObject.Name);

            var smashEvents = tournamentObject.Event.Where(x => x.Name.Contains(eventName));
            foreach (var smashEvent in smashEvents)
            {
                var stringBuilder = new StringBuilder();
                foreach (var standing in smashEvent.Standings)
                {
                    stringBuilder.AppendLine($"{standing.Ranking}: **{standing.Entrant.Name}**");
                }

                embedBuilder.AddField(smashEvent.Name, stringBuilder.ToString(), true);
            }

            await ReplyAsync(string.Empty, embed: embedBuilder.Build());
        }
    }
}
