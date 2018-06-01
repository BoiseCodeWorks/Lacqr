using Channels.Data.Interfaces;
using System.Collections.Generic;

namespace Channels.Data.Models
{
    internal class ChannelRoom : Room, IChannelRoom
    {
        public string ChannelId { get; set; }
    }
}