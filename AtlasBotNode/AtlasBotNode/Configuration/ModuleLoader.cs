using System;
using System.Collections.Generic;
using System.Linq;
using AtlasBotNode.Modules;
using AtlasBotNode.Modules.Help;
using AtlasBotNode.Modules.League;
using AtlasBotNode.Modules.Roles;
using AtlasBotNode.Modules.Smash;
using Discord.Commands;

namespace AtlasBotNode.Configuration
{
    /// <summary>
    ///     A helper class to load all the modules needed for this node.
    /// </summary>
    public class ModuleLoader
    {
        private readonly Dictionary<string, Type> _modules;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ModuleLoader" /> class.
        /// </summary>
        public ModuleLoader()
        {
            _modules = new Dictionary<string, Type>
            {
                {"League", typeof(LeagueModule)},
                {"Help", typeof(HelpModule)},
                {"Role", typeof(RoleModule)},
                {"Smash4", typeof(Smash4Module)},
                {"Smashgg", typeof(SmashggModule)},
                {"Test", typeof(TestModule)},
                {"ssg", typeof(SmashggNewModule)},
                {"ssbu", typeof(SmashUltimateModule)}
            };
        }

        /// <summary>
        ///     Gets a list of types that represent the modules needing to be loaded.
        /// </summary>
        /// <param name="identifiers">The identifiers of the modules wanting to load.</param>
        /// <returns>A collection of types that are derived from <see cref="ModuleBase" />.</returns>
        public IEnumerable<Type> GetModules(List<string> identifiers)
        {
            if (identifiers.Count == 1 && identifiers[0] == "All") return _modules.Select(x => x.Value);

            return _modules.Where(m => identifiers.Contains(m.Key)).Select(m => m.Value);
        }
    }
}