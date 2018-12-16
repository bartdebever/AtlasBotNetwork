using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace AtlasBotCommander.Communication
{
    /// <summary>
    /// The class to store information about the node.
    /// </summary>
    public class AtlasNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasNode"/> class.
        /// </summary>
        public AtlasNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AtlasNode"/> class.
        /// </summary>
        /// <param name="name">The name of the node.</param>
        /// <param name="socket">The socket to communicate with the node.</param>
        public AtlasNode(string name, Socket socket)
        {
            this.Name = name;
            this.Socket = socket;
        }

        /// <summary>
        /// Gets or sets the name of the node that is being communicated with.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the socket used for communicating with this node.
        /// </summary>
        public Socket Socket { get; set; }
    }
}
