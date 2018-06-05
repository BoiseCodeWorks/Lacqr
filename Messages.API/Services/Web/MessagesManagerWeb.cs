using System;
using System.Collections.Generic;
using System.Text;
using Messages.API.Interfaces;
using Messages.API.Models;
using Messages.Data.Interfaces;

namespace Messages.API.Services.Web
{
    public class MessagesManagerWeb : MessagesManager<IWebMessage>
    {
        public MessagesManagerWeb() : base("host=192.168.0.9;port=3306;database=lacqr;user id=student;password=student")
        { }

        public override IWebMessage Create(INewMessage m)
        {
            var message = _provider.Create(m);
            if (message == null) { throw new Exception("Invalid Message"); }
            var newWebMessage = new WebMessage(message);
            return newWebMessage;
        }

        public List<IWebMessage> GetMessages()
        {
            var rawMessages = _provider.GetMessages();
            List<IWebMessage> messages = new List<IWebMessage>();
            foreach (var m in rawMessages)
            {
                messages.Add(new WebMessage(m));
            }
            return messages;
        }

        public string Delete(string id)
        {
            var rawMessages = _provider.Delete(id);
            
            return rawMessages;
        }
    }
}
