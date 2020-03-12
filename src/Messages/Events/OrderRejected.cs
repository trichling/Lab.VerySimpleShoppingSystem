using System;
using NServiceBus;

namespace Messages.Events
{
    public class OrderRejected: IEvent
    {
        public Guid OrderId { get; set; }
        
    }
}