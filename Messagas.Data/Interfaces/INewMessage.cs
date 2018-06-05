using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Data.Interfaces
{
    public interface INewMessage
    {
        string UserId { get; set; }
        string RoomId { get; set; }
        string Content { get; set; }
        //string DateTime { get; set; }
    }
}
