using System.Text;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    using Discord;

    using YoutubeApiHandler.Results;

    public class YoutubeEmbedGenerator : IYoutubeEmbedGenerator
    {
        private EmbedBuilder _builder;

        public Embed Build()
        {
            return this._builder.Build();
        }

        private void ResetBuilder()
        {
            this._builder = new EmbedBuilder();
            this._builder.WithFooter("YouTube Module");
            this._builder.WithCurrentTimestamp();
            this._builder.WithColor(Color.Red);
        }

        public IYoutubeEmbedGenerator CreateChannelEmbed(YoutubeChannelResult channel)
        {
            this.ResetBuilder();
            this._builder.WithUrl(channel.Url);
            this._builder.AddField("Channel", $"{channel.Name} with {channel.SubscriberCount} subscribers.\n{channel.Url}");
            var stringBuilder = new StringBuilder();
            channel.Videos.ForEach(x => stringBuilder.AppendLine($"{x.Name}: {x.Url}"));
            this._builder.AddField("Most Popular Videos", stringBuilder.ToString());
            this._builder.WithThumbnailUrl(channel.IconUrl);

            return this;
        }
    }
}