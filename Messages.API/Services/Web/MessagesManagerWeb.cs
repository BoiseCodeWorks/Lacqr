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
        public MessagesManagerWeb() : base("server=192.168.0.2;port=3306;database=lacqr;user id=student;password=student")
        { }

        public override IWebMessage Create(INewMessage m)
        {
            var message = _provider.Create(m);
            if (message == null) { throw new Exception("Invalid Message"); }
            var newWebMessage = new WebMessage(message);
            return newWebMessage;
        }

    }
}
