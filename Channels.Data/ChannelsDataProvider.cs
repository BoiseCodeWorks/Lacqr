using Channels.Data.Interfaces;
using Channels.Data.Repositories;
using Channels.Data;
using System;

namespace Channels.Data
{
    public sealed class ChannelsDataProvider
    {
        internal ChannelsRepository _repo;
        public ChannelsDataProvider(string connectionString)
        {
            var x = new Messages.Data.DbConnectionOptions();
            _repo = new ChannelsRepository(x.GetMySqlConnection(connectionString));
        }
        public IChannel Create(INewChannel c)
        {
            return _repo.Create(c);
        }

    }
}
