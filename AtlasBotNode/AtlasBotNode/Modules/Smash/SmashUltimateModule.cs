using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace AtlasBotNode.Modules.Smash
{
    [Group("SSBU")]
    public class SmashUltimateModule : ModuleBase
    {
        [Command("character")]
        public async Task GetCharacter(string characterName)
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithImageUrl("https://www.ssbwiki.com/images/thumb/c/c1/Pichu_SSBU.png/250px-Pichu_SSBU.png");
            embedBuilder.WithThumbnailUrl(
                "https://www.ssbwiki.com/images/thumb/d/d6/PichuHeadSSBU.png/50px-PichuHeadSSBU.png");
            embedBuilder.WithCurrentTimestamp();
            embedBuilder.WithColor(Color.Blue);
            embedBuilder.WithTitle("Pichu");
            embedBuilder.AddField(
                "Description",
                "Pichu returns as a playable character in Super Smash Bros. Ultimate. This marks its first playable appearance in 17 years, a distinction shared with fellow Melee newcomer Young Link. Like Pikachu, Pichu now has a female variant to select from via alternate costumes; in Pichu\'s case, it is Spiky-eared Pichu. Unlike Pikachu Libre, Spiky-eared Pichu does not appear as a Spirit.");
            embedBuilder.AddField(
                "Updates",
                "**2.0.0:**\n- Pummel has less hitlag (15 frames → 14). This makes it faster and allows for an additional use at high percents before the opponent escapes.");
            embedBuilder.AddField("Notable Players", "Captain L, JaKaL, Nietono, VoiD, Yetey");
            await ReplyAsync(string.Empty, embed: embedBuilder.Build());
        }
    }
}