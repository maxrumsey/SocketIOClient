using System;
using Websocket.Client;

namespace SocketIOWSClient
{
    public class SocketIOClient : IDisposable
    {
        private Uri _uri;
        internal WebsocketClient Client;
        internal EngineIO EngineIO;
        internal SocketIO SocketIO;
        private Dictionary<string, Action<MessageData>> events = new();

        public void Dispose()
        {
            Client.Dispose();
        }

        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler Error;

        public SocketIOClient(string url)
        {
            var uriBuilder = new UriBuilder(url);
            uriBuilder.Scheme = "ws";
            uriBuilder.Path = "socket.io/";
            uriBuilder.Query = "?EIO=4&transport=websocket";

            _uri = uriBuilder.Uri;

            Client = new WebsocketClient(_uri);

            Client.ReconnectionHappened.Subscribe(ConnectedMethod);
            Client.DisconnectionHappened.Subscribe(DisconnectedMethod);
            Client.MessageReceived.Subscribe(MessagedMethod);

            EngineIO = new(this);
            SocketIO = new(this);
        }

        public async void Connect()
        {
            await Client.Start();
        }

        private void ConnectedMethod(ReconnectionInfo info)
        {
            
        }

        private void DisconnectedMethod(DisconnectionInfo info)
        {

        }

        private void MessagedMethod(ResponseMessage info)
        {
            if (info.MessageType == System.Net.WebSockets.WebSocketMessageType.Text && info.Text is not null)
            {
                EngineIO.ParseMessage(info.Text);
            }
        }
    }
}
