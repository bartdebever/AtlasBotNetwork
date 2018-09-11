using Discord;

namespace AtlasBotNode.EmbedGenerators
{
    public interface IEmbedGenerator
    {
        Embed Build();
    }
}