using System;
using NServiceBus;

namespace Messages.Events
{
    public class TermsAccepted: IEvent
    {
        public Guid OrderId { get; set; }
        
    }
}