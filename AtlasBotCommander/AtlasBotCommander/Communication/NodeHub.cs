using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using AtlasBotCommander.Loggers;
using AtlasModels.Logging;

namespace AtlasBotCommander.Communication
{
    using NLog;

    /// <summary>
    /// A class that manages all nodes and the commands that the nodes are allowed to send.
    /// </summary>
    public class NodeHub
    {
        /// <summary>
        /// The logger that is used 
        /// </summary>
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// A list of nodes that are active.
        /// </summary>
        private readonly List<AtlasNode> _nodes = new List<AtlasNode>();

        /// <summary>
        /// The Ip of the server.
        /// </summary>
        private readonly string _ip = "127.0.0.1";

        /// <summary>
        /// The port used for the server.
        /// </summary>
        private readonly int _port = 6677;

        /// <summary>
        /// The socket that is being listened to
        /// </summary>
        private Socket _listener;

        /// <summary>
        /// The token that clients need to authorize with.
        /// </summary>
        private string _token => "AtlasTestToken";

        /// <summary>
        /// Starts the listening process on the <see cref="_listener"/> socket.
        /// </summary>
        /// <exception cref="ArgumentNullException">When the IP is not set.</exception>
        public void Listen()
        {
            if (string.IsNullOrEmpty(_ip))
            {
                throw new ArgumentNullException(nameof(_ip));
            }

            var ipAddress = IPAddress.Parse(_ip);
            var localEndPoint = new IPEndPoint(ipAddress, _port);
            _listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(localEndPoint);
            _listener.Listen(100);
            _logger.Info($"Listening for nodes on {localEndPoint.Address} {_port}");
            while (true)
            {
                var socket = _listener.Accept();
                var node = new AtlasNode(_nodes.Count.ToString(), socket);
                _nodes.Add(node);
                new Thread(() => ListenForData(node)).Start();
            }
        }

        /// <summary>
        /// Waits and listens for data from the node.
        /// </summary>
        /// <param name="node">The node wanting to be listened to.</param>
        private void ListenForData(AtlasNode node)
        {
            while (true)
            {
                var data = new byte[1024 * 1024 * 50];
                try
                {
                    node.Socket.Receive(data);
                }
                catch (SocketException)
                {
                    _logger.Info($"{node.Name} Disconnected.");
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

        /// <summary>
        /// Handles the message given by the node.
        /// </summary>
        /// <param name="message">The message that has been send by the <paramref name="node"/></param>
        /// <param name="node">A reference to the node that send the message.</param>
        private void HandleMessage(LogMessage message, ref AtlasNode node)
        {
            switch (message.MessageType)
            {
                case 0:
                    if (message.Message != _token)
                    {
                        _nodes.Remove(node);
                        _logger.Warn($"Invalid token detected for ip {(node.Socket.RemoteEndPoint as IPEndPoint)?.Address}");
                        node.Socket.Disconnect(true);
                        return;
                    }

                    _logger.Info($"{message.Node} {message.Module}: {message.Message}");
                    node.Name = message.Node;
                    break;
                case 1:
                    _logger.Info($"{message.Node} {message.Module}: {message.Message}");
                    break;
            }
        }
    }
}
