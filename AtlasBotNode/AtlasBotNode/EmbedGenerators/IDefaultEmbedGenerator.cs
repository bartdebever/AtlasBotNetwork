namespace AtlasBotNode.EmbedGenerators
{
    public interface IDefaultEmbedGenerator : IEmbedGenerator
    {
        DefaultEmbedGenerator GenerateNotFoundEmbed(string module, string command, string title, string message);
    }
}