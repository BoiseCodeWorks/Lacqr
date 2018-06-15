using Channels.Data.Interfaces;
using Channels.Data.Repositories;
using Channels.Data;
using System;
using System.Collections.Generic;

namespace Channels.Data
{
    public sealed class ChannelsDataProvider
    {
        internal ChannelsRepository _repo;
        public ChannelsDataProvider(string connectionString)
        {
            var x = new DbConnectionOptions().GetMySqlConnection(connectionString);
            _repo = new ChannelsRepository(x);
        }

        public IChannel Create(INewChannel c)
        {
            return _repo.Create(c);
        }

        public IChannel GetChannel(ISubscriber sub)
        {
            return _repo.GetChannel(sub);
        }

        public IEnumerable<IChannel> GetAllChannels()
        {
            return _repo.GetAllChannels();
        }

        public IEnumerable<IChannelRoom> GetAllRooms()
        {
            return _repo.GetAllRooms();
        }

        public IEnumerable<IChannel> GetSubscribedChannels(string userId)
        {
            return _repo.GetSubscribedChannels(userId);
        }

        public void SubscribeToChannel(ISubscriber sub)
        {
            _repo.Subscribe(sub);
        }

        public void UnsubscribeFromChannel(ISubscriber sub)
        {
            _repo.Unsubscribe(sub);
        }

        public void CreateChannelRoom(INewRoom room)
        {
            _repo.CreateChannelRoom(room);
        }
    }
}
