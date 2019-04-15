using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using Discord;
using SmashggHandler.Exceptions;
using SmashggHandler.Models;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    public class SmashggEmbedGenerator : ISmashggEmbedGenerator
    {
        private EmbedBuilder _builder;

        public SmashggEmbedGenerator()
        {
            _builder = new EmbedBuilder();
        }

        public Embed Build()
        {
            return _builder.Build();
        }

        public ISmashggEmbedGenerator CreateTournamentEmbed(TournamentRoot tournament)
        {
            if (tournament?.Entities == null)
                throw new TournamentNotFoundException();
            ResetBuilder();
            _builder.WithColor(Color.Blue)
                .WithAuthor(new EmbedAuthorBuilder().WithName(tournament.Entities?.Tournament?.Name))
                .WithTimestamp(DateTimeOffset.Now)
                .AddField("General Info",
                    $"**Name:** {tournament.Entities?.Tournament?.Name}\n" +
                    $"**Venue:** {tournament.Entities?.Tournament?.VenueName}\n" +
                    $"**Starts at:** {tournament.Entities?.Tournament?.StartDate.ToLongDateString()}\n" +
                    $"**Ends at:** {tournament.Entities?.Tournament?.EndDate.ToLongDateString()}"
                );

            var profileImage = tournament.Entities?.Tournament?.Images?.FirstOrDefault(x => x.Type.Equals("profile"));
            if (profileImage != null) _builder.WithThumbnailUrl(profileImage.Url);

            var bannerImage = tournament.Entities?.Tournament?.Images?.FirstOrDefault(x => x.Type.Equals("banner"));
            if (bannerImage != null) _builder.WithImageUrl(bannerImage.Url);

            if (tournament.Entities.Events == null) return this;

            foreach (var gameEvent in tournament.Entities.Events.GroupBy(x => x.VideogameId))
            {
                var game = tournament.Entities.Videogames.FirstOrDefault(x => x.Id == gameEvent.Key) ??
                           new Videogame {Name = "Other"};
                var stringBuilder = new StringBuilder();
                foreach (var @event in gameEvent) stringBuilder.AppendLine(@event.Name);

                if (stringBuilder.Length > 0) _builder.AddField(game.Name, stringBuilder.ToString(), true);
            }

            return this;
        }

        public ISmashggEmbedGenerator CreateTournamentScheduleEmbed(TournamentScheduleRoot tournaments)
        {
            ResetBuilder();
            if (!tournaments.Items.Entities.Tournament.Any()) throw new TournamentNotFoundException();

            _builder.WithColor(Color.Blue)
                .WithTitle("Upcoming Tournaments on Smash.gg");
            foreach (var tournament in tournaments.Items.Entities.Tournament)
            {
                var stringBuilder = new StringBuilder();
                var events = tournaments.Items.Entities.Events.Where(x => x.TournamentId == tournament.Id);
                var games = tournaments.Items.Entities.Videogames.Where(x =>
                    events.FirstOrDefault(y => y.VideogameId == x.Id) != null);
                var videogames = games as IList<Videogame> ?? games.ToList();
                var max = videogames.Count() < 3 ? videogames.Count() : 3;
                for (var i = 0; i < max; i++)
                {
                    stringBuilder.AppendLine(videogames.ElementAt(i).Name);
                    if (i == max - 1 && max != videogames.Count())
                        stringBuilder.AppendLine($"And {videogames.Count() - max} more");
                }

                _builder.AddField(tournament.Name,
                    $"At {tournament.StartDate.ToLongDateString()}\n**Games:** \n{stringBuilder}", true);
            }

            return this;
        }

        public ISmashggEmbedGenerator CreateMatchEmbedGenerator()
        {
            ResetBuilder();
            _builder.WithTitle("Player1")
                .AddField("Tournament", "These games were played for tournament TournamentName\n" +
                                        "Use `-smashgg tournament TournamentName` to view more info.");
            for (var i = 0; i < 3; i++)
            {
                var stringBuilder = new StringBuilder();
                var random = new Random(i);
                for (var y = 0; y < random.Next(2, 5); y++)
                    stringBuilder.AppendLine($"**vs Player{y}:** {random.Next(0, 3)} - {random.Next(0, 3)}");

                _builder.AddField($"Game{i}", stringBuilder.ToString());
            }

            return this;
        }

        private void ResetBuilder()
        {
            _builder = new EmbedBuilder();
            _builder.WithColor(Color.Blue);
            _builder.WithFooter("Data provided by smash.gg");
        }
    }
}