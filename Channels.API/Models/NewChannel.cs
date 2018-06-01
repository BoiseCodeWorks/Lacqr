using System;
using System.Collections.Generic;
using System.Text;
using Channels.Data.Interfaces;

namespace Channels.API.Models
{
    public class NewChannel : INewChannel
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}
