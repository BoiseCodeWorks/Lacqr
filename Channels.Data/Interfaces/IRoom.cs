using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.Data.Interfaces
{
    public interface IRoom
    {
        string Id { get; set; }
        string Name { get; set; }
        string OwnerId { get; set; }
        List<string> Subscribers { get; set; }
        //List<Message> Messages { get; set; }
    }
}
