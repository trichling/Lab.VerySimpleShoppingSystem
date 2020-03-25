using NServiceBus;
using Messages.Events;
using System;
using System.Threading.Tasks;
using NServiceBus.Logging;

namespace ShoppingService
{
    public class PlaceOrderSagaData : ContainSagaData
    {
        public Guid OrderId { get; set; }

        public bool TermsAccepted { get; set; }
        public bool TermsRejected { get; set; }
        public bool PaymentDetailsProvided { get; set; }
    }

    public class PlaceOrderSaga : Saga<PlaceOrderSagaData>,
        IAmStartedByMessages<OrderPlaced>,
        IHandleMessages<TermsAccepted>,
        IHandleMessages<TermsRejected>,
        IHandleMessages<PaymentDetailsProvided>
    
    {

        

        public static ILog Log = LogManager.GetLogger<PlaceOrderSaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PlaceOrderSagaData> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(msg => msg.OrderId).ToSaga(d => d.OrderId);
            mapper.ConfigureMapping<TermsAccepted>(msg => msg.OrderId).ToSaga(d => d.OrderId);
            mapper.ConfigureMapping<TermsRejected>(msg => msg.OrderId).ToSaga(d => d.OrderId);
            mapper.ConfigureMapping<PaymentDetailsProvided>(msg => msg.OrderId).ToSaga(d => d.OrderId);
        }


        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            Log.Info($"Start place order process for order id {message.OrderId}");

            Data.OrderId = message.OrderId;
            return Task.CompletedTask;
        }

        public async Task Handle(TermsAccepted message, IMessageHandlerContext context)
        {
            Log.Info($"Terms have been accepted for order id {message.OrderId}");

            Data.TermsAccepted = true;
            await CheckForCompletion(context);
        }

        public async Task Handle(TermsRejected message, IMessageHandlerContext context)
        {
            Log.Info($"Terms have been rejected for order id {message.OrderId}");

            Data.TermsRejected = true;
            await CheckForCompletion(context);
        }

        public async Task Handle(PaymentDetailsProvided message, IMessageHandlerContext context)
        {
            Log.Info($"Payment detail was provided for order id {message.OrderId}");

            Data.PaymentDetailsProvided = true;
            await CheckForCompletion(context);
        }

        public async Task CheckForCompletion(IMessageHandlerContext context)
        {
            if (Data.TermsRejected)
            {
                Log.Info($"Order {Data.OrderId} was rejected due to rejected terms.");

                await context.Publish(new OrderRejected() {
                    OrderId = Data.OrderId
                });                

                return;
            }

            if (Data.TermsAccepted && Data.PaymentDetailsProvided)
            {
                Log.Info($"Order {Data.OrderId} was accepted");

                await context.Publish(new OrderAccepted() {
                    OrderId = Data.OrderId
                });
            }
        }

        
       
    }

    
}