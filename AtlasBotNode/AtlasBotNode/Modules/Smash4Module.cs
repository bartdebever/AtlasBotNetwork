using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using Discord.Commands;
using KhApiHandler;
using SmashggHandler.Models;
using System.Threading.Tasks;
using AtlasBotNode.Modules.Base;

namespace AtlasBotNode.Modules
{
    [Group("smash4")]
    [Alias("SB4", "S4")]
    public class Smash4Module : AtlasModule
    {
        private readonly KhClient _apiClient;
        private readonly ISmash4EmbedGenerator _smash4EmbedGenerator;

        public Smash4Module(ISmash4EmbedGenerator smash4EmbedGenerator)
        {
            _smash4EmbedGenerator = smash4EmbedGenerator;
            _apiClient = new KhClient();
        }

        public override string Identifier => "Smash4";
        
        [Command("character")]
        [Alias("c")]
        public async Task GetCharacterByName([Remainder] string characterName)
        {
            var character = await _apiClient.Characters.GetCharacterByName(characterName);
            var response = _smash4EmbedGenerator.CreateCharacterEmbed(character).Build();
            await ReplyAsync("", embed: response);
        }
    }
}