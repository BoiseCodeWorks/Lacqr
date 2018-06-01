using Channels.Data.Interfaces;
using System.Collections.Generic;

namespace Channels.Data.Models
{
    internal abstract class ChannelRoom : Room, IChannelRoom
    {
        public string ChannelId { get; set; }
    }
}