using System;
using NServiceBus;

namespace Messages.Comamnds
{
    public class CheckoutBasket : ICommand
    {
        public Guid UserId { get; set; }
   
    }
}