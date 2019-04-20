using System.Collections.Generic;
using System.Text;
using AtlasBotNode.EmbedGenerators.ModuleGenerators.Interfaces;
using Discord;
using Discord.Commands;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    /// <inheritdoc />
    public class HelpEmbedGenerator : IHelpEmbedGenerator
    {
        private readonly EmbedBuilder _embedBuilder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelpEmbedGenerator" /> class.
        /// </summary>
        public HelpEmbedGenerator()
        {
            _embedBuilder = new EmbedBuilder();
        }

        /// <inheritdoc />
        public Embed Build()
        {
            return _embedBuilder.Build();
        }

        /// <inheritdoc />
        public IHelpEmbedGenerator GenerateHelpListEmbed(IEnumerable<ModuleInfo> modules)
        {
            _embedBuilder.WithTitle("Command List")
                .WithColor(Color.Blue)
                .WithCurrentTimestamp();

            foreach (var module in modules)
            {
                var stringBuilder = new StringBuilder();
                foreach (var command in module.Commands)
                {
                    stringBuilder.Append($"**-{module.Name} {command.Name}**");
                    foreach (var parameter in command.Parameters) stringBuilder.Append($" *{parameter.Name}*");

                    stringBuilder.Append($": {command.Summary}\n");
                }
                _embedBuilder.AddField(module.Name, stringBuilder.ToString(), true);
            }

            return this;
        }
    }
}