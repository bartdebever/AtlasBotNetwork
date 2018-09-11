using System.Collections.Generic;

namespace AtlasModels.Server
{
    public class AtlasServerSettings
    {
        public List<AtlasModule> Modules { get; set; }
        public long BotCommanderRole { get; set; }
    }
}