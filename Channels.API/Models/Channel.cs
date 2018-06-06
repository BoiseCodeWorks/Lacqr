using System;
using System.Collections.Generic;
using System.Text;
using Channels.Data.Interfaces;

namespace Channels.API.Models
{
    public class Channel : IChannel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<string> Subscribers { get; set; }
        public List<string> Integrations { get; set; }
    }
}
