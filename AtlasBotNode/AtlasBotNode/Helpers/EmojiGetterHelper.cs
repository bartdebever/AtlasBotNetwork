using Discord;
using System.Collections.Generic;
using System.Linq;
using AtlasBotNode.Configuration;

namespace AtlasBotNode.Helpers
{
    public static class EmojiGetterHelper
    {
        private static readonly List<ulong> _emojiServers = new List<ulong> { 311780483912433665, 305289625667108865, 305291793635999744, 329165655108616202, 374597542177931269 };

        public static GuildEmote GetEmoji(string text)
        {
            if (text.Length.Equals(1))
                text += '_';
            foreach (var emojiServer in _emojiServers)
            {
                var server = DiscordCommandConfiguration.Client.Guilds.FirstOrDefault(x => x.Id == emojiServer);
                var emote =  server?.Emotes.FirstOrDefault(x => x.Name == text);
                if (emote != null)
                    return emote;
            }

            return null;

        }
    }
}