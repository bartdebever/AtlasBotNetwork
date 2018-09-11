using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using AtlasModels.Logging;
namespace AtlasBotNode.Communication
{
    public class CommanderConnector
    {
        public CommanderConnector(string ip, int port, string nodeName, string token)
        {
            _ip = ip;
            _port = port;
            _nodeName = nodeName;
            _token = token;
        }

        private readonly string _nodeName;
        private readonly string _token;
        private readonly string _ip;
        private readonly int _port;
        private Socket _workSocket;

        public void Connect()
        {
            _workSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _workSocket.Connect(_ip, _port);
            Console.WriteLine("Connected to host");
        }

        public void Register()
        {
            SendLogMessage(new LogMessage(_nodeName, null, _token, 0, 0));

        }
        public void LogMessage(string module, string message, int level)
        {
            SendLogMessage(new LogMessage(_nodeName, module, message, level, 1));
        }

        public Task LogDiscord(Discord.LogMessage logMessage)
        {
            var message = new LogMessage(_nodeName, null, logMessage.Message, 0, 1);
            SendLogMessage(message);
            return Task.CompletedTask;
        }

        private void SendLogMessage(LogMessage logMessage)
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