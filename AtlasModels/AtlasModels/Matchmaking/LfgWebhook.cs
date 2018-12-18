using System;
using System.Collections.Generic;
using System.Text;

namespace AtlasModels.Matchmaking
{
    public class LfgWebhook
    {

        public LfgWebhook()
        {
        }

        public LfgWebhook(long id, string token)
        {
            DiscordId = id;
            Token = token;
        }

        public int Id { get; set; }

        public long DiscordId { get; set; }

        public string Token { get; set; }

        public int Game { get; set; }
    }
}
