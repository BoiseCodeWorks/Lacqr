using Accounts.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Accounts.API.Models.Web;
using Accounts.API.Interfaces;

namespace Accounts.API.Services.Web
{
    public class AccountsManagerWeb : AccountsManager<IWebUser>
    {
        public AccountsManagerWeb() : base("server=localhost;port=3306;database=burgershack;user id=student;password=student")
        { }

        public override IWebUser Register(IAccountRegistration creds)
        {
            var user = new WebUser(_provider.Register(creds));
            user.SetClaims();
            return user;
        }

        public override IWebUser Login(IAccountLogin creds)
        {
            var user = new WebUser(_provider.Login(creds));
            if (user == null) { return null; }
            user.SetClaims();
            return user;
        }

        public IWebUser Authenticate(HttpContext ctx)
        {
            var ctxUser = ctx.User;
            var id = ctxUser.Identity.Name;
            var user = new WebUser(_provider.GetUserById(id));
            if (user == null) { return null; }
            user.SetClaims();
            return user;
        }
    }
}
