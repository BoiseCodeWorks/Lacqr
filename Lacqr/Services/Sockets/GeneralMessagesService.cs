using Accounts.API.Interfaces;
using Accounts.API.Services.Web;
using Accounts.Data.Interfaces;
using Channels.API.Services;
using Channels.Data.Interfaces;
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
        private ChannelsManager _cm;

        private ConcurrentDictionary<WebSocket, SocketUser> AllConnectedUsers = new ConcurrentDictionary<WebSocket, SocketUser>();
        private ConcurrentDictionary<string, SocketChannel> Channels = new ConcurrentDictionary<string, SocketChannel>();
        private ConcurrentDictionary<string, SocketRoom> Rooms = new ConcurrentDictionary<string, SocketRoom>();

        public GeneralMessagesService(WebSocketConnectionManager manager, AccountsManagerWeb am, ChannelsManager cm, MessagesManagerWeb mmw) : base(manager)
        {
            _mmw = mmw;
            _am = am;
            _cm = cm;
            foreach(var c in cm.GetAllChannels())
            {
                Channels.TryAdd(c.Id, new SocketChannel(c, _am));
            }
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
                    AllConnectedUsers.TryGetValue(socket, out SocketUser user);
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

            }
            catch (Exception e)
            {
                await SendMessageAsync(socket, "bad request");
            }
        }



        public override async void OnConnected(WebSocket socket, HttpContext context)
        {
            var user = _am.Authenticate(context);
            if (user == null) { socket.Abort(); return; }
            base.OnConnected(socket, context);
            var socketUser = new SocketUser(user, _cm, socket);
            AllConnectedUsers.TryAdd(socket, socketUser);
            foreach (var c in socketUser.SubscribedChannels)
            {
                Channels.TryGetValue(c.Id, out SocketChannel channel);
                channel.JoinChannel(socketUser);
            }

            var socketId = WSManager.GetId(socket);
            await SendMessageToAllAsync("{ \"type\": \"USERCONNECTED\", \"message\":\"" + user.Email + " is now connected\"}");
        }

        public override async Task OnDisconnected(WebSocket socket)
        {

            AllConnectedUsers.TryRemove(socket, out SocketUser socketUser);
            foreach(var c in socketUser.SubscribedChannels)
            {
                Channels.TryGetValue(c.Id, out SocketChannel channel);
                channel.LeaveChannel(socketUser);
            }
            //var socketId = WSManager.GetId(socket);
            await base.OnDisconnected(socket);
            //await SendMessageToAllAsync($"{socketId} has disconnected");
        }

    }

    public class SocketRoom
    {
        public IChannelRoom RoomData { get; set; }
        public IEnumerable<IChannelUser> Subscribers { get; set; }
        public IList<SocketUser> ConnectedUsers { get; set; }

        public SocketRoom(IChannelRoom roomData, AccountsManagerWeb am)
        {
            RoomData = roomData;
            Subscribers = am.GetSubscribers(roomData.Subscribers);
            ConnectedUsers = new List<SocketUser>();
        }


    }

    public class SocketUser
    {
        public WebSocket Socket { get; set; }
        public IWebUser User { get; set; }
        public IEnumerable<IChannel> SubscribedChannels { get; set; }

        public SocketUser(IWebUser user, ChannelsManager cm, WebSocket socket)
        {
            Socket = socket;
            User = user;
            SubscribedChannels = cm.GetSubscribedChannels(user.Id);
        }
    }

    public class SocketChannel
    {
        //private WebSocketHandler _handler;

        public IChannel ChannelData { get; set; }
        public IEnumerable<IChannelUser> Subscribers { get; set; }
        public IList<SocketUser> ConnectedUsers { get; set; }

        public SocketChannel(IChannel channelData, AccountsManagerWeb am)
        {
            ChannelData = channelData;
            //_handler = handler;
            Subscribers = am.GetSubscribers(channelData.Subscribers);
            ConnectedUsers = new List<SocketUser>();
        }

        public void JoinChannel(SocketUser socketUser)
        {
            if (ChannelData.Subscribers.Contains(socketUser.User.Id))
            {
               ConnectedUsers.Add(socketUser);
            }
        }

        public void LeaveChannel(SocketUser socketUser)
        {
            ConnectedUsers.Remove(socketUser);
        }
    }
}
