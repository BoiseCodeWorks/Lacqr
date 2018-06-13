using Accounts.API.Interfaces;
using Lacqr.Models;
using Messages.API.Models;
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
        public NewMessage Content { get; set; }
        public SocketUser User { get; set; }
        public string To { get; set; }

    }
}
