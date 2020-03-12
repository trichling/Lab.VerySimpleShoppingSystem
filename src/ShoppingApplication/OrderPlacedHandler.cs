using System.Threading.Tasks;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using static ShoppingApplication.Program;

namespace ShoppingApplication
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        public static ILog Log = LogManager.GetLogger<OrderPlacedHandler>();

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            OrderId = message.OrderId;
            OrderPlacedRecived.Set();
        }
    }
}