using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.Data.Interfaces
{
    public interface IChannelRoom : IRoom
    {
        string ChannelId { get; set; }
    }
}
