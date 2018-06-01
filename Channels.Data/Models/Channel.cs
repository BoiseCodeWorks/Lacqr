using Channels.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Channels.Data.Models
{
    internal class Channel : IChannel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public List<string> Subscribers { get; set; }
        public List<string> Integrations { get; set; }
    }
}
