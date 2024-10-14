using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.Timers;

namespace SocketIOWSClient
{
    internal class SocketIO
    {
        internal SocketIOClient SocketIOClient;
        private bool _opened;
        internal SocketIO(SocketIOClient socketIOClient)
        {
            SocketIOClient = socketIOClient;
            
        }

        internal void ParseMessage(string text)
        {
            if (text.Length < 1)
            {
                return;
            }

            var type = text[0];
            var data = text.Substring(1);

            switch(type)
            {
                case '0':
                    _opened = true;
                    break;
                case '2':
                    var el = JsonDocument.Parse(data);
                    break;
            }
        }

        internal void Connect()
        {
            SocketIOClient.EngineIO.SendMessage("0");
        }
    }
}
