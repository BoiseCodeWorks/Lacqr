
using Messages.Data;
using Messages.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.API.Services
{
    internal interface IMessagesManager<T>
    {
        T Create(INewMessage m);
    }

    public abstract class MessagesManager<T> : IMessagesManager<T>
    {
        internal MessagesDataProvider _provider;

        public MessagesManager(string connectionString)
        {
            _provider = new MessagesDataProvider(connectionString);
        }
        public virtual T Create(INewMessage m)
        {
            throw new NotImplementedException();
        }

    }
}
