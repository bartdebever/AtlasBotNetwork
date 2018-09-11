using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace AtlasBotCommander.Communication
{
    public class AtlasNode
    {
        public string Name { get; set; }
        public Socket Socket { get; set; }

        public AtlasNode()
        {
            
        }

        public AtlasNode(string name, Socket socket)
        {
            Name = name;
            Socket = socket;
        }
    }
}
