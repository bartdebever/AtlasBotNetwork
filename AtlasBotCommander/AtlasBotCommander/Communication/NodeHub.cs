﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using AtlasBotCommander.Loggers;
using AtlasModels.Logging;

namespace AtlasBotCommander.Communication
{
    public class NodeHub
    {
        private readonly List<AtlasNode> _nodes;
        private readonly string _ip = "127.0.0.1";
        private readonly int _port = 6677;
        private Socket _listener;
        private string _token => "AtlasTestToken";

        public NodeHub()
        {
            _nodes = new List<AtlasNode>();
        }

        public void Listen()
        {
            var ipAddress = IPAddress.Parse(_ip);
            var localEndPoint = new IPEndPoint(ipAddress, _port);
            _listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(localEndPoint);
            _listener.Listen(100);
            Console.WriteLine($"Listening for nodes on {localEndPoint.Address} {_port}");
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
                    DefaultLogger.Logger(new LogMessage(node.Name, null, "Disconnected", 1, 0));
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
            switch (message.MessageType)
            {
                case 0:
                    if (message.Message != _token)
                    {
                        _nodes.Remove(node);
                        DefaultLogger.Logger(new LogMessage(message.Node, message.Module, $"Invalid token detected for ip {(node.Socket.RemoteEndPoint as IPEndPoint)?.Address}", 1, 0));
                        node.Socket.Disconnect(true);
                        return;
                    }
                    DefaultLogger.Logger(new LogMessage(message.Node, message.Module, message.Message, 3, 0));
                    node.Name = message.Node;
                    break;
                case 1:
                    DefaultLogger.Logger(message);
                    break;
            }
        }
    }
}
