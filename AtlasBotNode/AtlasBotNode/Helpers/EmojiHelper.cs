using System.Collections.Generic;
using System.Linq;
using Discord;

namespace AtlasBotNode.Helpers
{
    /// <summary>
    ///     An helper that will load or work with custom emojis or other emoji business.
    /// </summary>
    public static class EmojiHelper
    {
        private static readonly List<ulong> _emojiServers = new List<ulong>
            {311780483912433665, 305289625667108865, 305291793635999744, 329165655108616202, 374597542177931269};

        /// <summary>
        ///     Gets the emoji from the Discord servers specified in the <see cref="_emojiServers" />.
        /// </summary>
        /// <param name="text">The name that should be searched for.</param>
        /// <returns>The <see cref="GuildEmote" /> that corresponds to the given <paramref name="text" /> or null.</returns>
        public static GuildEmote GetEmoji(string text)
        {
            // If the emote text is only 1 character it needs an underscore (_) appended to it.
            if (text.Length.Equals(1)) text += '_';

            foreach (var emojiServer in _emojiServers)
            {
                var server = DiscordCommandHelper.Client.Guilds.FirstOrDefault(x => x.Id == emojiServer);
                var emote = server?.Emotes.FirstOrDefault(x => x.Name == text);

                // If the server or the emote is not found the emote variable will be null.
                if (emote != null) return emote;
            }

            return null;
        }
    }
}