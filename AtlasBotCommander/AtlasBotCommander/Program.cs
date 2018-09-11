using System;
using System.Linq;
using System.Threading;
using AtlasBotCommander.Communication;
using AtlasBotCommander.Loggers;

namespace AtlasBotCommander
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
    using Microsoft.Extensions.DependencyInjection;

    namespace AtlasBotNode
    {
        public class Program
        {
            private NodeHub _nodeHub;

            private static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

            private async Task Start()
            {
                _nodeHub = new NodeHub();
                new Thread(() => { _nodeHub.Listen(); }).Start();
                await Task.Delay(-1);
            }
        }

    }

}
