using System;
using NServiceBus;

namespace Messages.Events
{
    public class OrderAccepted : IEvent
    {
        public Guid OrderId { get; set; }
        
    }
}