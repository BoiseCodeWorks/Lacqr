using Channels.Data.Interfaces;

namespace Channels.API.Models
{
    public class Subscriber : ISubscriber
    {
        public string SubscribableId { get; set; }
        public string UserId { get; set; }

        public Subscriber(string id, string userId)
        {
            SubscribableId = id;
            UserId = userId;
        }

    }
}