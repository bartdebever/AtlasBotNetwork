﻿using System.Threading.Tasks;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using Discord.Commands;
using YoutubeApiHandler;

namespace AtlasBotNode.Modules.YouTube
{
    /// <summary>
    /// Module made to get data from the YouTube API.
    /// </summary>
    [Group("Youtube")]
    public class YoutubeModule : ModuleBase
    {
        private readonly YoutubeClient _youtubeClient;

        private readonly IYoutubeEmbedGenerator _youtubeEmbedGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeModule"/> class.
        /// </summary>
        /// <param name="youtubeEmbedGenerator">
        /// Automatically injected
        /// </param>
        public YoutubeModule(IYoutubeEmbedGenerator youtubeEmbedGenerator)
        {
            this._youtubeClient = new YoutubeClient();
            this._youtubeEmbedGenerator = youtubeEmbedGenerator;
        }

        /// <summary>
        /// Gets the channel by name provided by the user
        /// </summary>
        /// <param name="name">The name of channel wanting to be gotten</param>
        /// <returns>A task.</returns>
        [Command("channel")]
        public async Task GetChannel([Remainder] string name)
        {
            var channel = await this._youtubeClient.GetYouTuberByNameAsync(name);
            if (channel == null)
            {
                await ReplyAsync("Couldn't find that channel, sorry!");
                return;
            }
            await ReplyAsync(string.Empty, embed: this._youtubeEmbedGenerator.CreateChannelEmbed(channel).Build());
        }
    }
}