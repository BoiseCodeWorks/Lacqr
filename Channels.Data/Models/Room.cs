using System.Collections.Generic;

namespace Channels.Data.Models
{
    internal class Room
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public List<string> Subscribers { get; set; }
    }
}