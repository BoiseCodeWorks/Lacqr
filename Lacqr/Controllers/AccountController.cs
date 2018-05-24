using System;
using System.Threading.Tasks;
using Accounts.API.Interfaces;
using Accounts.API.Models;
using Accounts.API.Models.Web;
using Accounts.API.Services.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lacqr.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private AccountsManagerWeb _manager;

        public AccountController(AccountsManagerWeb m)
        {
            _manager = m;
        }

        [HttpPost("register")]
        public async Task<IWebUser> Register([FromBody]AccountRegistration creds)
        {
            if (ModelState.IsValid)
            {
                IWebUser user = _manager.Register(creds);
                if (user != null)
                {
                    await SetUserSession(user);
                    return user;
                }
            }
            throw new Exception("Invalid Credentials");
        }

        [HttpPost("login")]
        public async Task<IWebUser> Login([FromBody]AccountLogin creds)
        {
            if (ModelState.IsValid)
            {
                IWebUser user = _manager.Login(creds);
                if (user != null)
                {
                    await SetUserSession(user);
                    return user;
                    
                }
            }
            throw new Exception("Invalid Credentials");
        }

        [HttpGet("authenticate")]
        public IWebUser Authenticate()
        {
            return _manager.Authenticate(HttpContext);
        }

        private async Task<IWebUser> SetUserSession(IWebUser user)
        {
            await HttpContext.SignInAsync(user.GetPrincipal());
            return user;
        }
    }
}
