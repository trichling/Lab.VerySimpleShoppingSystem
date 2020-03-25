using System;
using System.Threading.Tasks;
using Messages.Comamnds;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace ShoppingService
{
    public class CheckoutBasketHandler: IHandleMessages<CheckoutBasket>
    {
        public static ILog Log = LogManager.GetLogger<CheckoutBasketHandler>();

        public CheckoutBasketHandler(/* Parameter werden per DI injiziert */)
        {
            
        }

        public async Task Handle(CheckoutBasket message, IMessageHandlerContext context)
        {
            var orderId = Guid.NewGuid();
            Log.Info($"Checking out basket of user {message.UserId}, creating order {orderId}");
            
            await context.Publish(new OrderPlaced()
            {
                UserId = message.UserId,
                OrderId = orderId
            });
        }
    }
   
}