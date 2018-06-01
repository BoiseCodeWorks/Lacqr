using Channels.Data.Interfaces;
using Channels.Data.Repositories;
using System;

namespace Channels.Data
{
    public sealed class RoomsDataProvider
    {
        internal RoomsRepository _repo;
        public RoomsDataProvider(string connectionString)
        {
            var x = new Messages.Data.DbConnectionOptions();
            _repo = new RoomsRepository(x.GetMySqlConnection(connectionString));
        }
        public IRoom Create(INewRoom r)
        {
            return _repo.Create(r);
        }

    }
}
