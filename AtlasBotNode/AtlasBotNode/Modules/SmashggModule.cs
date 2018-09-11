﻿using AtlasBotNode.EmbedGenerators;
using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using AtlasBotNode.Helpers;
using Discord;
using Discord.Commands;
using SmashggHandler;
using System.Linq;
using System.Threading.Tasks;

namespace AtlasBotNode.Modules
{
    [Group("Smashgg")]
    public class SmashggModule : ModuleBase
    {
        private readonly SmashggClient _smashggClient;
        private readonly IInputSanitizer _inputSanitizer;
        private readonly IDefaultEmbedGenerator _defaultEmbedGenerator;
        private readonly ISmashggEmbedGenerator _smashggEmbedGenerator;

        public SmashggModule(IInputSanitizer inputSanitizer, IDefaultEmbedGenerator defaultEmbedGenerator, ISmashggEmbedGenerator smashggEmbedGenerator)
        {
            _smashggClient = new SmashggClient();
            _inputSanitizer = inputSanitizer;
            _defaultEmbedGenerator = defaultEmbedGenerator;
            _smashggEmbedGenerator = smashggEmbedGenerator;
        }

        [Command("Tournament")]
        [Summary("Get the info from a tournament by name")]
        public async Task GetTournamentInfoByName([Remainder] string name)
        {
            Embed response;
            name = _inputSanitizer.SmashggTournamentReplacement(name);
            var tournament = await _smashggClient.TournamentEndpoint.GetTournamentByName(name);
            if (tournament == null)
                response = _defaultEmbedGenerator.GenerateNotFoundEmbed("Smashgg", "Tournament",
                    "Tournament has not been found!", "The tournament you were looking for has not been found").Build();
            else
                response = _smashggEmbedGenerator.CreateTournamentEmbed(tournament).Build();
            await ReplyAsync("", embed: response);
        }

        [Command("Upcoming")]
        [Summary("Get the upcoming tournaments on Smash.gg")]
        public async Task GetUpcomingTournaments()
        {
            Embed response;
            var tournaments = await _smashggClient.TournamentEndpoint.GetUpcomingTournaments();
            if (tournaments == null || !tournaments.Items.Entities.Tournament.Any())
                response = _defaultEmbedGenerator.GenerateNotFoundEmbed("Smashgg", "Tournament",
                    "No upcoming tournaments", "There are no upcoming tournaments on Smash.gg :(").Build();
            else
                response = _smashggEmbedGenerator.CreateTournamentScheduleEmbed(tournaments).Build();
            await ReplyAsync("", embed: response);
        }
    }
}