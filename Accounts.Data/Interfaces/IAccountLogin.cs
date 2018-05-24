using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Interfaces
{
    public interface IAccountLogin
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
