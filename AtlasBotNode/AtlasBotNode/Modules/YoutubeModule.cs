using AtlasBotNode.Modules.Base;

namespace AtlasBotNode.Modules
{
    using AtlasBotNode.EmbedGenerators.ModuleGenerators;
    using Discord.Commands;
    using System.Threading.Tasks;
    using YoutubeApiHandler;

    /// <summary>
    /// Module made to get data from the YouTube API.
    /// </summary>
    [Group("Youtube")]
    public class YoutubeModule : AtlasModule
    {
        private readonly YoutubeRequester _youtubeRequester;

        private readonly IYoutubeEmbedGenerator _youtubeEmbedGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="YoutubeModule"/> class.
        /// </summary>
        /// <param name="youtubeEmbedGenerator">
        /// Automatically injected
        /// </param>
        public YoutubeModule(IYoutubeEmbedGenerator youtubeEmbedGenerator)
        {
            this._youtubeRequester = new YoutubeRequester();
            this._youtubeEmbedGenerator = youtubeEmbedGenerator;
        }

        public override string Identifier => "Youtube";
        
        /// <summary>
        /// Gets the channel by name provided by the user
        /// </summary>
        /// <param name="name">The name of channel wanting to be gotten</param>
        /// <returns>A task.</returns>
        [Command("channel")]
        public async Task GetChannel([Remainder] string name)
        {
            var channel = await this._youtubeRequester.GetYoutuberByNameAsync(name);
            if (channel == null)
            {
                await ReplyAsync("Couldn't find that channel, sorry!");
                return;
            }
            await ReplyAsync(string.Empty, embed: this._youtubeEmbedGenerator.CreateChannelEmbed(channel).Build());
        }
    }
}