﻿using Messages.Data.Interfaces;
using Messages.Data.Repositories;
using System;

namespace Messages.Data
{
    public sealed class MessagesDataProvider
    {
        internal MessagesRepository _repo;
        public MessagesDataProvider(string connectionString)
        {
            var x = new DbConnectionOptions();
            _repo = new MessagesRepository(x.GetMySqlConnection(connectionString));
        }
        public IMessage Create(INewMessage m)
        {
            return _repo.Create(m);
        }

    }
}
