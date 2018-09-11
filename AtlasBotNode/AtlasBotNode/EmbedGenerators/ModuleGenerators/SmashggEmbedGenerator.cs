﻿using Discord;
using SmashggHandler.Exceptions;
using SmashggHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    public interface ISmashggEmbedGenerator : IEmbedGenerator
    {
        ISmashggEmbedGenerator CreateTournamentEmbed(TournamentRoot tournament);

        ISmashggEmbedGenerator CreateTournamentScheduleEmbed(TournamentScheduleRoot tournaments);
    }

    public class SmashggEmbedGenerator : ISmashggEmbedGenerator
    {
        public SmashggEmbedGenerator()
        {
            _builder = new EmbedBuilder();
        }

        private EmbedBuilder _builder;

        public Embed Build()
        {
            return _builder.Build();
        }

        private void ResetBuilder()
        {
            _builder = new EmbedBuilder();
        }

        public ISmashggEmbedGenerator CreateTournamentEmbed(TournamentRoot tournament)
        {
            if (tournament?.Entities == null)
                throw new TournamentNotFoundException();
            ResetBuilder();
            _builder.WithColor(Color.Blue);
            _builder.WithAuthor(new EmbedAuthorBuilder().WithName(tournament.Entities?.Tournament?.Name));
            _builder.WithTimestamp(DateTimeOffset.Now);
            _builder.AddField("General Info",
                $"**Name:** {tournament.Entities?.Tournament?.Name}\n" +
                $"**Venue:** {tournament.Entities?.Tournament?.VenueName}\n" +
                $"**Starts at:** {tournament.Entities?.Tournament?.StartDate.ToLongDateString()}\n" +
                $"**Ends at:** {tournament.Entities?.Tournament?.EndDate.ToLongDateString()}"
                );
            var profileImage = tournament.Entities?.Tournament?.Images?.FirstOrDefault(x => x.Type.Equals("profile"));
            if (profileImage != null)
                _builder.WithThumbnailUrl(profileImage.Url);
            var bannerImage = tournament.Entities?.Tournament?.Images?.FirstOrDefault(x => x.Type.Equals("banner"));
            if (bannerImage != null)
                _builder.WithImageUrl(bannerImage.Url);
            if (tournament.Entities.Events == null)
                return this;
            foreach (var gameEvent in tournament.Entities.Events.GroupBy(x => x.VideogameId))
            {
                var game = tournament.Entities.Videogames.FirstOrDefault(x => x.Id == gameEvent.Key) ?? new Videogame { Name = "Other" };
                var stringBuilder = new StringBuilder();
                foreach (var @event in gameEvent)
                {
                    stringBuilder.AppendLine(@event.Name);
                }
                if (stringBuilder.Length > 0)
                    _builder.AddInlineField(game.Name, stringBuilder.ToString());
            }
            return this;
        }

        public ISmashggEmbedGenerator CreateTournamentScheduleEmbed(TournamentScheduleRoot tournaments)
        {
            ResetBuilder();
            if (!tournaments.Items.Entities.Tournament.Any())
                throw new TournamentNotFoundException();
            _builder.WithColor(Color.Blue);
            _builder.WithTitle("Upcoming Tournaments on Smash.gg");
            foreach (var tournament in tournaments.Items.Entities.Tournament)
            {
                var stringBuilder = new StringBuilder();
                var events = tournaments.Items.Entities.Events.Where(x => x.TournamentId == tournament.Id);
                var games = tournaments.Items.Entities.Videogames.Where(x => events.FirstOrDefault(y => y.VideogameId == x.Id) != null);
                var videogames = games as IList<Videogame> ?? games.ToList();
                var max = videogames.Count() < 3 ? videogames.Count() : 3;
                for (var i = 0; i < max; i++)
                {
                    stringBuilder.AppendLine(videogames.ElementAt(i).Name);
                    if (i == max - 1 && max != videogames.Count())
                        stringBuilder.AppendLine($"And {videogames.Count() - max} more");
                }
                _builder.AddInlineField(tournament.Name, $"At {tournament.StartDate.ToLongDateString()}\n**Games:** \n{stringBuilder}");
            }
            return this;
        }
    }
}