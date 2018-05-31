using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.Data.Models
{
    internal class ChannelUserRole
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
