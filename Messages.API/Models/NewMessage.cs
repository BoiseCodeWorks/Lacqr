using System;
using System.Collections.Generic;
using System.Text;
using Messages.Data.Interfaces;

namespace Messages.API.Models
{
    public class NewMessage : IMessage
    {
        public string UserId { get; set; }
        public string RoomId { get; set; }
        public string Content { get; set; }
    }
}
