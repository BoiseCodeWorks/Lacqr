using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.Data.Interfaces
{
    public interface INewRoom
    {
        string Name { get; set; }
        string OwnerId { get; set; }
        string ChannelId { get; set; }
    }
}
