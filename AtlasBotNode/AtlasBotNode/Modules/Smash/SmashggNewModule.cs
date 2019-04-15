using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using SmashggNet;
using SmashggNet.Models;

namespace AtlasBotNode.Modules.Smash
{
    [Group("sgg")]
    public class SmashggNewModule : ModuleBase
    {
        private const int MAX_STANDINGS = 3;

        private static Embed TournamentNotFoundEmbed(string tournamentName)
        {
            var builder = new EmbedBuilder();
            builder.WithColor(Color.Red);
            builder.WithTitle("Error.");
            builder.AddField("Not found!", $"No tournament found with the name `{tournamentName}`.");
            return builder.Build();
        }

        #region Standings

        [Command("standings")]
        [Description("Gets the current standings or final results for a tournament.")]
        public async Task GetTournamentStandings(string tournament)
        {
            var tournamentObject = await SmashggClient.Tournament.GetTournamentStandings(tournament);

            if (tournamentObject == null)
            {
                await ReplyAsync(string.Empty, embed: TournamentNotFoundEmbed(tournament));
                return;
            }

            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle(tournamentObject.Name);

            var amountOfStandings = tournamentObject.Event.Count;

            if (amountOfStandings > MAX_STANDINGS)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Too many events, please refine your search.");
                stringBuilder.AppendLine("**Events:**");
                foreach (var name in tournamentObject.Event.Select(x => x.Name)) stringBuilder.AppendLine($" -{name}");

                embedBuilder.AddField("Too many events!", stringBuilder.ToString());
            }
            else
            {
                StandingsEmbedForEvents(tournamentObject.Event, ref embedBuilder);
            }

            await ReplyAsync(string.Empty, embed: embedBuilder.Build());
        }

        [Command("standings")]
        [Description("Gets the current standings or final results for a tournament for a specific event name.")]
        public async Task GetTournamentStandings(string eventName, [Remainder] string tournament)
        {
            var tournamentObject = await SmashggClient.Tournament.GetTournamentStandings(tournament);

            if (tournamentObject == null)
            {
                await ReplyAsync(string.Empty, embed: TournamentNotFoundEmbed(tournament));
                return;
            }

            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithTitle(tournamentObject.Name);

            var smashEvents = tournamentObject.Event.Where(x => x.Name.Contains(eventName));

            StandingsEmbedForEvents(smashEvents, ref embedBuilder);

            await ReplyAsync(string.Empty, embed: embedBuilder.Build());
        }

        private static void StandingsEmbedForEvents(IEnumerable<Event> events, ref EmbedBuilder builder)
        {
            foreach (var smashEvent in events)
            {
                var stringBuilder = new StringBuilder();
                foreach (var standing in smashEvent.Standings)
                    stringBuilder.AppendLine($"{standing.Ranking}: **{standing.Entrant.Name}**");

                builder.AddField(smashEvent.Name, stringBuilder.ToString(), true);
            }
        }

        #endregion
    }
}