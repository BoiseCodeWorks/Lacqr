using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Lacqr.Services.Sockets
{
    public class GeneralMessagesService : WebSocketHandler
    {
        public GeneralMessagesService(WebSocketConnectionManager manager) : base(manager) { }

        public async override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var id = WSManager.GetId(socket);
            var message = GetMessageFromByteArray(result, buffer);
            await SendMessageToAllAsync(message);
        }

        public override async void OnConnected(WebSocket socket)
        {
            base.OnConnected(socket);

            var socketId = WSManager.GetId(socket);
            await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            var id = WSManager.GetId(socket);
            await base.OnDisconnected(socket);
            await SendMessageToAllAsync($"{id} has disconnected");
        }

    }
}
