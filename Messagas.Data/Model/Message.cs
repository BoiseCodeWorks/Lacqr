using Messages.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Data.Model
{
    internal class Message : IMessage
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string RoomId { get; set; }
    }
}
