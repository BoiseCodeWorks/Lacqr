using Accounts.Data.Interfaces;
using Channels.Data.Interfaces;
using Lacqr.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lacqr.Models
{
    public class ChannelDetail : IChannelDetails
    {
        public string Id {get; set;}
        public string Name {get; set;}
        public string OwnerId {get; set;}
        public IEnumerable<IChannelUser> Subscribers {get; set;}
        public List<string> Integrations {get; set;}

        public ChannelDetail(IChannel c, IEnumerable<IChannelUser> users)
        {
            Id = c.Id;
            Name = c.Name;
            OwnerId = c.OwnerId;
            Subscribers = users;
        }

    }
}
