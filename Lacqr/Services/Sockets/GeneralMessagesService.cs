using Messages.API.Services.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Lacqr.Services.Sockets
{
    public class GeneralMessagesService : WebSocketHandler
    {
        private MessagesManagerWeb _mmw;
        public GeneralMessagesService(WebSocketConnectionManager manager, MessagesManagerWeb mmw) : base(manager) { _mmw = mmw; }

        public async override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var id = WSManager.GetId(socket);
            var message = GetMessageFromByteArray(result, buffer);
            try
            {
                SocketMessage m = JsonConvert.DeserializeObject<SocketMessage>(message);
                if (m != null)
                {
                    _mmw.Create(m.Content);
                    switch (m.Type)
                    {
                        case "BROADCASTMESSAGE":
                            await SendMessageToAllAsync(message);
                            break;
                        case "PRIVATEMESSAGE":
                            await SendMessageAsync(m.To, message);
                            break;
                        default:
                            await SendMessageAsync(socket, "bad request");
                            break;
                    }
                }

            }catch(Exception e)
            {
                await SendMessageAsync(socket, "bad request");
            }
        }

        public override async void OnConnected(WebSocket socket)
        {
            base.OnConnected(socket);

            var socketId = WSManager.GetId(socket);
            await SendMessageToAllAsync("{ \"type\": \"USERCONNECTED\", \"message\":\"" + socketId + "is now connected\"}");
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            var socketId = WSManager.GetId(socket);
            await base.OnDisconnected(socket);
            await SendMessageToAllAsync($"{socketId} has disconnected");
        }

    }
}
