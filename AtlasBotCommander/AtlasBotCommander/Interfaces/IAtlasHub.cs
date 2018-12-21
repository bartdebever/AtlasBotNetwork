using System;
using System.Collections.Generic;
using System.Text;

namespace AtlasBotCommander.Interfaces
{
    public interface IAtlasHub
    {
        void Setup();

        void Connect();

        void Subscribe();
    }
}
