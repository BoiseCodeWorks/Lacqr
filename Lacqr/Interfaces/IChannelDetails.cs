using Accounts.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lacqr.Interfaces
{
    public interface IChannelDetails
    {
        string Id { get; set; }
        string Name { get; set; }
        string OwnerId { get; set; }
        IEnumerable<IChannelUser> Subscribers { get; set; }
        List<string> Integrations { get; set; }
    }
}
