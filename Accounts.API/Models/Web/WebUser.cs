using Accounts.API.Interfaces;
using Accounts.Data.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace Accounts.API.Models.Web
{
    internal class WebUser : IWebUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool AccountConfirmed { get; set; }
        internal ClaimsPrincipal Principal { get; set; }

        public WebUser(IUser u)
        {
            Id = u.Id;
            Email = u.Email;
            Username = u.Username;
            AccountConfirmed = u.AccountConfirmed;
        }

        internal void SetClaims()
        {
            var claims = new List<Claim> {
                        new Claim(ClaimTypes.Email, Email),
                        new Claim(ClaimTypes.Name, Id.ToString())
                    };
            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            Principal = principal;
        }

        public ClaimsPrincipal GetPrincipal()
        {
            return Principal;
        }
    }
}
