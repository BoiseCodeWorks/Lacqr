using Accounts.Data.Interfaces;
using Accounts.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Data
{
    public class AccountsDataProvider
    {
        internal AccountsRepository _repo;
        public AccountsDataProvider(string connectionString)
        {
            var x = new DbConnectionOptions();
            _repo = new AccountsRepository(x.GetMySqlConnection(connectionString));
        }

        public IUser Register(IAccountRegistration creds)
        {
            return _repo.Register(creds);
        }

        public IUser Login(IAccountLogin creds)
        {
            return _repo.Login(creds);
        }

        public bool ChangePassword(IAccountPasswordChange change)
        {
            return _repo.ChangeUserPassword(change);
        }

        public IUser GetUserById(string id)
        {
            return _repo.GetUserById(id);
        }
    }
}
