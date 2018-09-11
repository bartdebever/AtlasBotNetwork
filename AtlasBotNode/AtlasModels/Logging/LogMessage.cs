using System;
using System.Collections.Generic;
using System.Text;

namespace AtlasModels.Logging
{
    [Serializable]
    public class LogMessage
    {
        public LogMessage()
        {
            
        }

        public LogMessage(string node, string module, string message, int logLevel, int type)
        {
            Node = node;
            Module = module;
            Message = message;
            LogLevel = logLevel;
            MessageType = type;
        }
        public string Node { get; set; }
        public string Module { get; set; }
        public string Message { get; set; }
        public int LogLevel { get; set; }
        public int MessageType { get; set; }
    }
}
