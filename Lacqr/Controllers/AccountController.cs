using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.API.Interfaces;
using Accounts.API.Models;
using Accounts.API.Services;
using Accounts.Data.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Lacqr.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private AccountsManager _manager;

        public AccountController(AccountsManager m)
        {
            _manager = m;
        }

        [HttpPost("register")]
        public IWebUser Register([FromBody]AccountRegistration creds)
        {
            if (ModelState.IsValid)
            {
                IWebUser user = _manager.Register(creds);
                if (user != null)
                {
                    return user;
                    //return await SetUserSession(user);
                }
            }
            throw new Exception("Invalid Credentials");
        }

        [HttpPost("login")]
        public async Task<IWebUser> Login([FromBody]IAccountLogin creds)
        {
            if (ModelState.IsValid)
            {
                IWebUser user = _manager.Login(creds);
                if (user != null)
                {
                    return await SetUserSession(user);
                }
            }
            return null;
        }

        [HttpGet("authenticate")]
        public IWebUser Authenticate()
        {
            return _manager.Authenticate(HttpContext);
        }

        private async Task<IWebUser> SetUserSession(IWebUser user)
        {
            await HttpContext.SignInAsync(user.Principal);
            return user;
        }
    }
}
