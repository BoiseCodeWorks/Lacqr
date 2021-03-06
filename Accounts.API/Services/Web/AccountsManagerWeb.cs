﻿using Accounts.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Accounts.API.Models.Web;
using Accounts.API.Interfaces;
using System;
using System.Collections.Generic;

namespace Accounts.API.Services.Web
{
    public class AccountsManagerWeb : AccountsManager<IWebUser>
    {
        public AccountsManagerWeb() : base("server=localhost;port=3306;database=lacqr;user id=student;password=student")
        { }

        public override IWebUser Register(IAccountRegistration creds)
        {
            var auth = _provider.Register(creds);
            if (auth == null) { throw new Exception("Invalid Credentials"); }
            var user = new WebUser(auth);
            user.SetClaims();
            return user;
        }

        public override IWebUser Login(IAccountLogin creds)
        {
            var auth = _provider.Login(creds);
            if (auth == null) { throw new Exception("Invalid Credentials"); }
            var user = new WebUser(auth);
            user.SetClaims();
            return user;
        }

        public IWebUser Authenticate(HttpContext ctx)
        {
            try
            {
                var ctxUser = ctx.User;
                var id = ctxUser.Identity.Name;
                var user = new WebUser(_provider.GetUserById(id));
                user.SetClaims();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<IChannelUser> GetSubscribers(IEnumerable<string> subscribers)
        {
            var subs = new List<IChannelUser>();
            foreach (var id in subscribers)
            {
                subs.Add(_provider.GetUserById(id));
            }
            return subs;
        }
    }
}
