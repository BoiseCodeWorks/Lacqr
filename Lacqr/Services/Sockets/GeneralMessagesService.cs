using Accounts.API.Interfaces;
using Accounts.API.Services.Web;
using Messages.API.Services.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Lacqr.Services.Sockets
{
    public class GeneralMessagesService : WebSocketHandler
    {
        private MessagesManagerWeb _mmw;
        private AccountsManagerWeb _am;
        private IWebUser _user;

        private ConcurrentDictionary<WebSocket, IWebUser> _userSocketManager = new ConcurrentDictionary<WebSocket, IWebUser>();

        public GeneralMessagesService(WebSocketConnectionManager manager, AccountsManagerWeb am, MessagesManagerWeb mmw) : base(manager) {
            _mmw = mmw;
            _am = am;
        }

        public async override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var id = WSManager.GetId(socket);
            var message = GetMessageFromByteArray(result, buffer);
            try
            {
                SocketMessage m = JsonConvert.DeserializeObject<SocketMessage>(message);
                if (m != null)
                {
                    _userSocketManager.TryGetValue(socket, out IWebUser user);
                    m.Content.UserId = user.Id;
                    //_mmw.Create(m.Content);
                    switch (m.Type)
                    {
                        case "BROADCASTMESSAGE":
                            await SendMessageToAllAsync(message);
                            break;
                        case "PRIVATEMESSAGE":
                            await SendMessageAsync(m.To, message);
                            break;
                        case "COMMAND":
                            await SendMessageToAllAsync(message);
                            break;
                        default:
                            await SendMessageAsync(socket, "{ \"error\": \"bad request\"}");
                            break;
                    }
                }

            }catch(Exception e)
            {
                await SendMessageAsync(socket, "bad request");
            }
        }



        public override async void OnConnected(WebSocket socket, HttpContext context)
        {
            var user = _am.Authenticate(context);
            if (user == null) { socket.Abort(); return; }
            base.OnConnected(socket, context);
            _user = user;
            _userSocketManager.TryAdd(socket, user);
            var socketId = WSManager.GetId(socket);
            await SendMessageToAllAsync("{ \"type\": \"USERCONNECTED\", \"message\":\"" + user.Email + " is now connected\"}");
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            var socketId = WSManager.GetId(socket);
            _userSocketManager.Remove(socket, out IWebUser user);
            await base.OnDisconnected(socket);
            await SendMessageToAllAsync($"{socketId} has disconnected");
        }

    }
}
