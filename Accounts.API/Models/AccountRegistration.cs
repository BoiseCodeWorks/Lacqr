using Accounts.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.API.Models
{
    public class AccountRegistration : IAccountRegistration
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
