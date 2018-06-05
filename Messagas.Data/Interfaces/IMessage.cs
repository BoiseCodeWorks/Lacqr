using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Data.Interfaces
{
    public interface IMessage
    {
        string UserId { get; set; }
        string RoomId { get; set; }
        string Content { get; set; }
        DateTime DateTime { get; set; }
    }
}
