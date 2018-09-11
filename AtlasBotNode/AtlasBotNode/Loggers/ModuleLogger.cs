using Discord;
using System;
using System.Threading.Tasks;

namespace AtlasBotNode.Loggers
{
    public static class ModuleLogger
    {
        public static Task Logger(LogSeverity severity, string module, string command, string message)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{severity,8}] {module}/{command} : {message}");
            Console.ResetColor();

            return Task.CompletedTask;
        }
    }
}