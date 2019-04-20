using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace AtlasBotNode.Modules.Roles
{
    [Group("Role")]
    [Alias("region", "rank")]
    public class RoleModule : ModuleBase
    {
        [Command("")]
        public async Task GetRoles([Remainder] string roleText)
        {
            if (Context.Guild.Id != 227778876540059651) return;

            var roleNames = roleText.Split(" ");
            var roleList = roleNames
                .Select(roleName =>
                    Context.Guild.Roles.FirstOrDefault(x =>
                        x.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase)))
                .Where(role => role != null).ToList();
            if (Context.User is IGuildUser guildUser) await guildUser.AddRolesAsync(roleList);

            var roleStringBuilder = new StringBuilder("Roles given to you:");
            roleList.ForEach(x => roleStringBuilder.Append(" " + x.Name));
            await ReplyAsync(roleStringBuilder.ToString());
        }
    }
}