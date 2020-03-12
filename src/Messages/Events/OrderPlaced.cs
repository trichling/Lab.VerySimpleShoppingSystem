using System;
using NServiceBus;

namespace Messages.Events
{
    public class OrderPlaced: IEvent
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}