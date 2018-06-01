using System;
using System.Collections.Generic;
using System.Text;
using Channels.Data.Interfaces;

namespace Channels.API.Models
{
    public class PeerRoom : IRoom
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public List<string> Subscribers { get; set; }
    }
}
