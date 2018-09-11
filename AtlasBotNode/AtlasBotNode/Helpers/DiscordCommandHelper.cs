﻿using Discord.Commands;
using Discord.WebSocket;
using System;

namespace AtlasBotNode.Helpers
{
    public static class DiscordCommandHelper
    {
        public static CommandService CommandService { get; set; }
        public static DiscordSocketClient Client { get; set; }
        public static IServiceProvider Services { get; set; }
    }
}