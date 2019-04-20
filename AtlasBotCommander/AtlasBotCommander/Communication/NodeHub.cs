using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using AtlasBotCommander.AtlasBotNode;
using AtlasModels.Logging;
using Microsoft.Extensions.Logging;

namespace AtlasBotCommander.Communication
{
    public class NodeHub
    {
        private readonly List<AtlasNode> _nodes;
        private readonly string _ip = "127.0.0.1";
        private readonly int _port = 6677;
        private readonly ILogger<NodeHub> _logger;
        private Socket _listener;
        private string _token => "AtlasTestToken";

        public NodeHub(ILogger<NodeHub> logger)
        {
            _logger = logger;
            _nodes = new List<AtlasNode>();
        }

        public void Listen()
        {
            var ipAddress = IPAddress.Parse(_ip);
            var localEndPoint = new IPEndPoint(ipAddress, _port);
            _listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(localEndPoint);
            _listener.Listen(100);
            _logger.LogInformation($"Listening for nodes on {localEndPoint.Address} {_port}");
            while (true)
            {
                var socket = _listener.Accept();
                var node = new AtlasNode(_nodes.Count.ToString(), socket);
                _nodes.Add(node);
                new Thread(() => ListenForData(node)).Start();
            }
        }

        private void ListenForData(AtlasNode node)
        {
            var interupted = false;
            while (!interupted)
            {
                var data = new byte[1024 * 1024 * 50];
                try
                {
                    node.Socket.Receive(data);
                }
                catch (SocketException)
                {
                    var message = $"{node.Name} disconnected";
                    _logger.LogWarning(message);
                    Program.LogMessage($"**{message}**").GetAwaiter().GetResult();
                    _nodes.Remove(node);

                    return;
                }

                using (var memoryStream = new MemoryStream(data))
                {
                    var formatter = new BinaryFormatter();
                    var message = (LogMessage)formatter.Deserialize(memoryStream);
                    HandleMessage(message, ref node);
                }
            }

        }

        private void HandleMessage(LogMessage message, ref AtlasNode node)
        {
            string textMessage;
            switch (message.MessageType)
            {
                case 0:
                    if (message.Message != _token)
                    {
                        textMessage =
                            $"{message.Node} tried to register with an invalid token, IP: {(node.Socket.RemoteEndPoint as IPEndPoint)?.Address}";
                        _nodes.Remove(node);
                        _logger.LogCritical(textMessage);
                        Program.LogMessage(textMessage).GetAwaiter().GetResult();
                        node.Socket.Disconnect(true);
                        return;
                    }
                    textMessage = $"[{message.Node}] [{message.Module}]: {message.Message}";
                    _logger.LogInformation(textMessage);
                    node.Name = message.Node;
                    break;
                case 1:
                    textMessage = $"[{message.Node}] [{message.Module}]: {message.Message}";
                    _logger.LogInformation(textMessage);
                    Program.LogMessage(textMessage).GetAwaiter().GetResult();
                    break;
            }
        }
    }
}
