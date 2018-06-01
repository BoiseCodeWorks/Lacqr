using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.Data.Models
{
    enum Role
    {
        Owner,
        Subscriber
    }

    internal class ChannelUserRole
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string UserId { get; set; }
        public Role Role { get; set; }
    }
}
