using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Data.Interfaces
{
    public interface IUser : IChannelUser
    {
        bool AccountConfirmed { get; set; }
    }
    public interface IChannelUser
    {
        string Id { get; set; }
        string Email { get; set; }
        string Username { get; set; }
    }
}
