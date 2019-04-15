using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Discord;

namespace AtlasBotNode.Communication
{
    public class CommanderConnector
    {
        private readonly string _ip;

        private readonly string _nodeName;
        private readonly int _port;
        private readonly string _token;
        private Socket _workSocket;

        public CommanderConnector(string ip, int port, string nodeName, string token)
        {
            _ip = ip;
            _port = port;
            _nodeName = nodeName;
            _token = token;
        }

        public void Connect()
        {
            _workSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _workSocket.Connect(_ip, _port);
            Console.WriteLine("Connected to host");
        }

        public void Register()
        {
            SendLogMessage(new AtlasModels.Logging.LogMessage(_nodeName, null, _token, 0, 0));
        }

        public void LogMessage(string module, string message, int level)
        {
            SendLogMessage(new AtlasModels.Logging.LogMessage(_nodeName, module, message, level, 1));
        }

        public Task LogDiscord(LogMessage logMessage)
        {
            var level = 0;
            switch (logMessage.Severity)
            {
                case LogSeverity.Warning:
                    level = 2;
                    break;
                case LogSeverity.Error:
                case LogSeverity.Critical:
                    level = 1;
                    break;
            }

            var message = new AtlasModels.Logging.LogMessage(_nodeName, null, logMessage.Message, level, 1);
            SendLogMessage(message);
            return Task.CompletedTask;
        }

        private void SendLogMessage(AtlasModels.Logging.LogMessage logMessage)
        {
            byte[] messageInBytes;
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, logMessage);
                messageInBytes = memoryStream.ToArray();
            }

            _workSocket.Send(messageInBytes);
        }
    }
}