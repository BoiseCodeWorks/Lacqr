using Accounts.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Models
{
    internal class User : IUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool AccountConfirmed { get; set; }
    }
}
