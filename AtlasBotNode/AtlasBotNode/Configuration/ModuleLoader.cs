using System;
using System.Collections.Generic;
using System.Linq;
using AtlasBotNode.Modules;
using Discord.Commands;

namespace AtlasBotNode.Configuration
{
    /// <summary>
    /// A helper class to load all the modules needed for this node.
    /// </summary>
    public class ModuleLoader
    {
        private readonly Dictionary<string, Type> _modules;

        public ModuleLoader()
        {
            _modules = new Dictionary<string, Type>()
            {
                {"League", typeof(LeagueModule)},
                {"Help", typeof(HelpModule)},
                {"Quiz", typeof(QuizModule)},
                {"Role", typeof(RoleModule)},
                {"Smash4", typeof(Smash4Module)},
                {"Smashgg", typeof(SmashggModule)},
                {"Speedrun", typeof(SpeedrunModule)},
                {"Test", typeof(TestModule)},
                {"YouTube", typeof(YoutubeModule)}
            };
        }
        
        /// <summary>
        /// Gets a list of types that represent the modules needing to be loaded.
        /// </summary>
        /// <param name="identifiers">The identifiers of the modules wanting to load.</param>
        /// <returns>A collection of types that are derived from <see cref="ModuleBase"/></returns>
        public IEnumerable<Type> GetModules(List<string> identifiers)
        {
            return _modules.Where(m => identifiers.Contains(m.Key)).Select(m => m.Value);
        }
    }
}