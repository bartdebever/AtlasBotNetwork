using Discord;
using Discord.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasBotNode.Modules
{
    [Group("Role")]
    [Alias("region", "rank")]
    public class RoleModule : ModuleBase
    {
        [Command("")]
        public async Task GetRoles([Remainder] string roleText)
        {
            var roleNames = roleText.Split(" ");
            var roleList = roleNames.Select(roleName => Context.Guild.Roles.FirstOrDefault(x => x.Name.Equals(roleName))).Where(role => role != null).ToList();
            if (!roleList.Any())
                return;
            if (Context.User is IGuildUser guildUser)
            {
                await guildUser.AddRolesAsync(roleList);
            }
            var roleStringBuilder = new StringBuilder("Roles given to you:");
            roleList.ForEach(x => roleStringBuilder.Append(" " + x.Name));
            await ReplyAsync(roleStringBuilder.ToString());
        }
    }
}