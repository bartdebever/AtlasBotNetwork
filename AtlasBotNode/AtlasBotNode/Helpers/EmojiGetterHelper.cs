using Discord;
using System.Collections.Generic;
using System.Linq;

namespace AtlasBotNode.Helpers
{
    public static class EmojiGetterHelper
    {
        private static readonly List<ulong> _emojiServers = new List<ulong> { 311780483912433665 };

        public static GuildEmote GetEmoji(string text)
        {
            var server = DiscordCommandHelper.Client.Guilds.FirstOrDefault(x => x.Id == _emojiServers[0]);
            return server?.Emotes.FirstOrDefault(x => x.Name == text);
        }
    }
}