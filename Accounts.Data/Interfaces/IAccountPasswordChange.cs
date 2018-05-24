using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Interfaces
{
    public interface IAccountPasswordChange
    {
        string Id { get; set; }
        string OldPassword { get; set; }
        string NewPassword { get; set; }
    }
}
