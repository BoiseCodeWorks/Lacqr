using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Interfaces
{
    public interface IUser
    {
        string Id { get; set; }
        string Email { get; set; }
        string Username { get; set; }
        bool AccountConfirmed { get; set; }
    }
}
