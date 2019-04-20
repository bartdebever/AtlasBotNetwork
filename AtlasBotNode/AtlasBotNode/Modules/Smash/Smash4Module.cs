using System.Threading.Tasks;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using Discord.Commands;
using KhApiHandler;

namespace AtlasBotNode.Modules.Smash
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
            var character = await _apiClient.Characters.GetCharacterByNameAsync(characterName);
            var response = _smash4EmbedGenerator.CreateCharacterEmbed(character).Build();
            await ReplyAsync(string.Empty, embed: response);
        }
    }
}