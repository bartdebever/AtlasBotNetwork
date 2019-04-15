using System.Text;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using Discord;
using YoutubeApiHandler.Results;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    public class YoutubeEmbedGenerator : IYoutubeEmbedGenerator
    {
        private EmbedBuilder _builder;

        public Embed Build()
        {
            return _builder.Build();
        }

        public IYoutubeEmbedGenerator CreateChannelEmbed(YoutubeChannelResult channel)
        {
            ResetBuilder();
            _builder.WithUrl(channel.Url);
            _builder.AddField("Channel", $"{channel.Name} with {channel.SubscriberCount} subscribers.\n{channel.Url}");

            var stringBuilder = new StringBuilder();
            channel.Videos.ForEach(x => stringBuilder.AppendLine($"{x.Name}: {x.Url}"));

            _builder.AddField("Most Popular Videos", stringBuilder.ToString());
            _builder.WithThumbnailUrl(channel.IconUrl);

            return this;
        }

        private void ResetBuilder()
        {
            _builder = new EmbedBuilder()
                .WithFooter("YouTube Module")
                .WithCurrentTimestamp()
                .WithColor(Color.Red);
        }
    }
}