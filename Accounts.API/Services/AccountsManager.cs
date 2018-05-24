using Accounts.Data;
using Accounts.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.API.Services
{
    internal interface IAccountsManager<T>
    {
        T Register(IAccountRegistration creds);
        T Login(IAccountLogin creds);
    }
    public abstract class AccountsManager<T> : IAccountsManager<T>
    {
        internal AccountsDataProvider _provider;
        public AccountsManager(string connectionString)
        {
            _provider = new AccountsDataProvider(connectionString);
        }

        public virtual T Login(IAccountLogin creds)
        {
            throw new NotImplementedException();
        }

        public virtual T Register(IAccountRegistration creds)
        {
            throw new NotImplementedException();
        }
    }

}
