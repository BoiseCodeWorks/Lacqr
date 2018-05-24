using Accounts.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Accounts.API.Interfaces
{
    public interface IWebUser : IUser
    {
        ClaimsPrincipal Principal { get; set; }
    }
}
