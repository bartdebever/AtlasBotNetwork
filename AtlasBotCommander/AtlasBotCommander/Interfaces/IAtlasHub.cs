using System;
using System.Collections.Generic;
using System.Text;

namespace AtlasBotCommander.Interfaces
{
    public interface IAtlasHub
    {
        /// <summary>
        /// Sets up the hub to be ready to start dealing with transactions.
        /// </summary>
        void Setup();

        /// <summary>
        /// Connects to the service.
        /// </summary>
        void Connect();

        /// <summary>
        /// Subscribes to all the needed services so it can be used.
        /// </summary>
        void Subscribe();
    }
}
