using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Accounts.Data.Interfaces
{
    public interface IAccountRegistration
    {
        [EmailAddress]
        string Email { get; set; }
        [MinLength(6)]
        string Password { get; set; }
        string Username { get; set; }
    }
}
