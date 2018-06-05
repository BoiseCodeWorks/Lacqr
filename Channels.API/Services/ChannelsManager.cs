using Channels.API.Models;
using Channels.Data;
using Channels.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.API.Services
{
    public class ChannelsManager
    {
        private ChannelsDataProvider _provider;

        public ChannelsManager()
        {
            _provider = new ChannelsDataProvider("server=192.168.0.9;port=3306;database=lacqr;user id=student;password=student");
        }

        public IEnumerable<IChannel> GetSubscribedChannels(string confirmedUserId)
        {
            return _provider.GetSubscribedChannels(confirmedUserId);
        }

        public IChannel GetChannel(ISubscriber sub)
        {
            return _provider.GetChannel(sub);
        }

        public IChannel CreateChannel(INewChannel channel)
        {
            return _provider.Create(channel);
        }

        public void SubscribeToChannel(Subscriber sub)
        {
            _provider.SubscribeToChannel(sub);
        }

        public void UnsubscribeFromChannel(Subscriber sub)
        {
            _provider.UnsubscribeFromChannel(sub);
        }
    }
}
