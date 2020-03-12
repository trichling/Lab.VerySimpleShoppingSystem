using System.Threading.Tasks;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using static ShoppingApplication.Program;

namespace ShoppingApplication
{
     public class OrderAcceptedOrRejectedHandler : IHandleMessages<OrderAccepted>, IHandleMessages<OrderRejected>
    {
        public static ILog Log = LogManager.GetLogger<OrderPlacedHandler>();

        public async Task Handle(OrderAccepted message, IMessageHandlerContext context)
        {
            HasOrderBeenAccepted = true;
            OrderAcceptedOrRejectedReceived.Set();
        }

        public async Task Handle(OrderRejected message, IMessageHandlerContext context)
        {
            HasOrderBeenAccepted = false;
            OrderAcceptedOrRejectedReceived.Set();
        }
    }
}