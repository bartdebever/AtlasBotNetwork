using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using LogMessage = AtlasModels.Logging.LogMessage;

namespace AtlasBotCommander.Loggers
{
    public static class DefaultLogger
    {
        public static void Logger(LogMessage message)
        {
            switch (message.LogLevel)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{message.Node,8}] {message.Module}: {message.Message}");
            Console.ResetColor();
        }
    }
}
