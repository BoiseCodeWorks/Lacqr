using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.Data.Interfaces
{
    public interface ISubscriber
    {
        string SubscribableId { get; set; }
        string UserId { get; set; }
    }
}
