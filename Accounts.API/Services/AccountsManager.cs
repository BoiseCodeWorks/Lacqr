using Accounts.API.Models;
using Accounts.Data;
using Accounts.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Accounts.API.Interfaces;
using System.Collections.Generic;

namespace Accounts.API.Services
{
    public class AccountsManager
    {
        private AccountsDataProvider _provider;

        public AccountsManager()
        {
            _provider = new AccountsDataProvider("server=localhost;port=3306;database=burgershack;user id=student;password=student;");
        }

        public IWebUser Register(IAccountRegistration creds)
        {
            //Business logic
            var user = new WebUser(_provider.Register(creds));
            if (user == null) { return null; }
            user.SetClaims();
            return user;
        }

        public IWebUser Login(IAccountLogin creds)
        {
            var user =  new WebUser(_provider.Login(creds));
            if (user == null) { return null; }
            user.SetClaims();
            return user;
        }

        public IWebUser Authenticate(HttpContext ctx)
        {
            var ctxUser = ctx.User;
            var id = ctxUser.Identity.Name;
            var user = new WebUser(_provider.GetUserById(id));
            if(user == null) { return null; }
            user.SetClaims();
            return user;
        }
    }
}
