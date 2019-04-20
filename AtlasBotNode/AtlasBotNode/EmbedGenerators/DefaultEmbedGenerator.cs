using Discord;

namespace AtlasBotNode.EmbedGenerators
{
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
            _embedBuilder.AddField(title, message)
                .WithColor(Color.Red)
                .WithTitle("Not found");

            return this;
        }
    }
}