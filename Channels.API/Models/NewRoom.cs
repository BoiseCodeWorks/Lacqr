using System;
using System.Collections.Generic;
using System.Text;
using Channels.Data.Interfaces;

namespace Channels.API.Models
{
    public class NewRoom : INewRoom
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}
