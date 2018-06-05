using System;
using System.Collections.Generic;
using System.Text;
using Messages.API.Interfaces;
using Messages.Data.Interfaces;

namespace Messages.API.Models
{
    public class WebMessage : IWebMessage
    {
        public string UserId { get; set; }
        public string RoomId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public WebMessage(IMessage m)
        {
            UserId = m.UserId;
            RoomId = m.RoomId;
            Content = m.Content;
            DateTime = m.DateTime;
        }
    }

}
