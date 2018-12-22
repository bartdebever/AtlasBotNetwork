using System;
using System.Collections.Generic;
using System.Linq;
using AtlasBotNode.Modules;

namespace AtlasBotNode.Configuration
{
    public class ModuleLoader
    {
        private readonly Dictionary<string, Type> _modules;

        public ModuleLoader()
        {
            _modules = new Dictionary<string, Type>()
            {
                {"League", typeof(LeagueModule)},
                {"Help", typeof(HelpModule)}
            };
        }
        
        public List<Type> GetModules(List<string> identifiers)
        {
            return _modules.Where(m => identifiers.Contains(m.Key)).Select(m => m.Value).ToList();
        }
        
        
    }
}