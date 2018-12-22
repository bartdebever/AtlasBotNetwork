using AtlasBotNode.EmbedGenerators.ModuleGenerators;
using Discord.Commands;
using KhApiHandler;
using SmashggHandler.Models;
using System.Threading.Tasks;

namespace AtlasBotNode.Modules
{
    [Group("smash4")]
    [Alias("SB4", "S4")]
    public class Smash4Module : ModuleBase
    {
        private readonly KhClient _apiClient;
        private readonly ISmash4EmbedGenerator _smash4EmbedGenerator;

        public Smash4Module(ISmash4EmbedGenerator smash4EmbedGenerator)
        {
            _smash4EmbedGenerator = smash4EmbedGenerator;
            _apiClient = new KhClient();
        }

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