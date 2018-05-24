using Accounts.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Models
{
    internal class Account : IAccount
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public IProfile Profile { get; set; }

        internal IUser GetUser()
        {
            return new User()
            {
                Id = Id,
                Email = Email,
                Username = Username
            };
        }
    }
}
