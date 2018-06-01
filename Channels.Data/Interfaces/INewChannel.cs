using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.Data.Interfaces
{
    public interface INewChannel
    {
        string Name { get; set; }
        string OwnerId { get; set; }
    }
}
