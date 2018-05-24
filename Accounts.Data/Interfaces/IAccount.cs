using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Interfaces
{
    internal interface IAccount
    {
        string Id { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Username { get; set; }
        IProfile Profile { get; set; }
    }
}
