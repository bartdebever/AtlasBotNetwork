using Discord.Commands;

namespace AtlasBotNode.Modules.Base
{
    public abstract class AtlasModule : ModuleBase
    {
        public abstract string Identifier { get; }
    }
}