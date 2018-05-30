using System;
using System.Collections.Generic;
using System.Text;
using Messages.Data.Interfaces;

namespace Messages.API.Services.Web
{
    public class MessagesManagerWeb : MessagesManager<IMessage>
    {
        public MessagesManagerWeb() : base("server=localhost;port=3306;database=lacqr;user id=student;password=student")
        { }
    public override IMessage Create(IMessage creds)
        {
            var message = _provider.Create(creds);
            if (message == null) { throw new Exception("Invalid Message"); }
            return message;
        }

    }
}
