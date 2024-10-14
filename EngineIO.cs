using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Globalization;

namespace SocketIOWSClient
{
    internal class EngineIO
    {
        private System.Timers.Timer intervalTimer = new System.Timers.Timer();
        internal string? Sid;
        internal int PingInterval;
        internal int PingTimeout;
        internal int MaxPayload;
        internal SocketIOClient SocketIOClient;

        internal EngineIO(SocketIOClient socketIOClient)
        {
            SocketIOClient = socketIOClient;

            intervalTimer.Elapsed += TimeoutElapsed;
            
        }

        private void TimeoutElapsed(object? sender, ElapsedEventArgs e)
        {

            SocketIOClient.Client.Reconnect();
            intervalTimer.Stop();
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
                    var args = JsonSerializer.Deserialize<EngineIOConnectionPayload>(data);
                    PingInterval = args.pingInterval;
                    PingTimeout = args.pingTimeout;
                    MaxPayload = args.maxPayload;

                    intervalTimer.Interval = intervalTimer.Interval = PingInterval + PingTimeout;
                    intervalTimer.Start();

                    SocketIOClient.SocketIO.Connect();
                    break;
                case '1':
                    throw new Exception("Socket was closed.");
                case '2':
                    SocketIOClient.Client.Send("3");
                    intervalTimer.Stop();
                    intervalTimer.Start();
                    break;
                case '4':
                    SocketIOClient.SocketIO.ParseMessage(data);
                    break;
            }
        }

        internal void SendMessage(string text)
        {
            SocketIOClient.Client.Send("4" + text);
        }
    }
}
