using Channels.Data.Interfaces;
using System.Collections.Generic;

namespace Channels.Data.Models
{
    internal abstract class Room : IRoom
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public List<string> Subscribers { get; set; }
        //public List<Message> Messages { get; set; }
    }
}