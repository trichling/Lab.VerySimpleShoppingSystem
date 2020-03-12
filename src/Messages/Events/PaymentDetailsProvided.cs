using System;
using NServiceBus;

namespace Messages.Events
{
    public class PaymentDetailsProvided : IEvent
    {
        public Guid OrderId { get; set; }
        
    }
}