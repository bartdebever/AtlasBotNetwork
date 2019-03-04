using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using Discord;
using KhApiHandler.Models;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    public class Smash4EmbedGenerator : ISmash4EmbedGenerator
    {
        private EmbedBuilder _embedBuilder;

        public ISmash4EmbedGenerator CreateCharacterEmbed(Character character)
        {
            ResetEmbedBuilder();

            _embedBuilder.WithTitle(character.Name)
                .WithUrl(character.FullUrl)
                .WithImageUrl(character.MainImageUrl)
                .WithThumbnailUrl(character.ThumbnailUrl);

            if (!string.IsNullOrEmpty(character.Description))
            {
                _embedBuilder.AddField("Description", character.Description);
            }

            _embedBuilder.AddField("KH", "Data provided by: http://kuroganehammer.com");
            return this;
        }

        private void ResetEmbedBuilder()
        {
            _embedBuilder = new EmbedBuilder()
                .WithColor(Color.DarkGreen)
                .WithCurrentTimestamp()
                .WithFooter("Smash4 Module");
        }

        public Embed Build()
        {
            return _embedBuilder.Build();
        }
    }
}