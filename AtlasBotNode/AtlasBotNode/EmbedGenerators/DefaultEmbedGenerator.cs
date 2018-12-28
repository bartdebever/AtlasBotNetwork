using AtlasBotNode.Loggers;
using Discord;

namespace AtlasBotNode.EmbedGenerators
{
    public interface IDefaultEmbedGenerator : IEmbedGenerator
    {
        DefaultEmbedGenerator GenerateNotFoundEmbed(string module, string command, string title, string message);
    }

    public class DefaultEmbedGenerator : IDefaultEmbedGenerator
    {
        private readonly EmbedBuilder _embedBuilder;

        public DefaultEmbedGenerator()
        {
            _embedBuilder = new EmbedBuilder();
        }

        public Embed Build()
        {
            return _embedBuilder.Build();
        }

        public DefaultEmbedGenerator GenerateNotFoundEmbed(string module, string command, string title, string message)
        {
            _embedBuilder.AddField(title, message);
            _embedBuilder.WithColor(Color.Red);
            _embedBuilder.WithTitle("Not found");
            return this;
        }
    }
}