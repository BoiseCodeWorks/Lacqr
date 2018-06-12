using Messages.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lacqr.Services.Sockets
{
    public class SocketMessage
    {
        public string Type { get; set; }
        public INewMessage Content { get; set; }
        public string To { get; set; }
    }
}
