using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Text;

namespace AtlasBotNode.EmbedGenerators.ModuleGenerators
{
    public interface IHelpEmbedGenerator : IEmbedGenerator
    {
        IHelpEmbedGenerator GenerateHelpListEmbed(IEnumerable<ModuleInfo> modules);
    }

    public class HelpEmbedGenerator : IHelpEmbedGenerator
    {
        private readonly EmbedBuilder _embedBuilder;

        public HelpEmbedGenerator()
        {
            _embedBuilder = new EmbedBuilder();
        }

        public Embed Build()
        {
            return _embedBuilder.Build();
        }

        public IHelpEmbedGenerator GenerateHelpListEmbed(IEnumerable<ModuleInfo> modules)
        {
            _embedBuilder.WithTitle("Command List");
            _embedBuilder.WithColor(Color.Blue);
            _embedBuilder.WithCurrentTimestamp();

            foreach (var module in modules)
            {
                var stringBuilder = new StringBuilder();
                foreach (var command in module.Commands)
                {
                    stringBuilder.Append($"**-{module.Name} {command.Name}**");
                    foreach (var parameter in command.Parameters)
                    {
                        stringBuilder.Append($" *{parameter.Name}*");
                    }
                    stringBuilder.Append($": {command.Summary}\n");
                }
                _embedBuilder.AddInlineField(module.Name, stringBuilder.ToString());
            }

            return this;
        }
    }
}