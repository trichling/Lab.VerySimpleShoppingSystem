using System;
using NServiceBus;

namespace Messages.Events
{
    public class TermsRejected : IEvent
    {
        public Guid OrderId { get; set; }
        
    }
}