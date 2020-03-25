using System;
using System.Threading;
using System.Threading.Tasks;
using Messages;
using Messages.Comamnds;
using Messages.Events;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.FluentConfiguration.Core;

namespace ShoppingApplication
{
    class Program
    {
        public static Guid OrderId = Guid.Empty;
        public static ManualResetEvent OrderPlacedRecived = new ManualResetEvent(false);
        public static ManualResetEvent OrderAcceptedOrRejectedReceived = new ManualResetEvent(false);
        public static bool HasOrderBeenAccepted = false;

        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigureNServiceBus()
                        .WithEndpoint("ShoppingApplication")
                        .WithTransport<LearningTransport>()
                        .WithRouting(r => {})
                        .WithPersistence<LearningPersistence>();

            var endpointInstance = configurationBuilder
                .ManageEndpoint()
                .Start()
                .Instance;



            var userId = Guid.NewGuid();

            Console.WriteLine($"Hello user {userId}. Please type an article number to add to the basket");
            Console.Write("> ");
            var articleNumber = Console.ReadLine();

            await endpointInstance.Send("ShoppingService", new AddArticleToBasket()
            {
                UserId = userId,
                ArticleNumber = articleNumber,
                Quantity = 1
            });

            Console.WriteLine("Press Enter to checkout basket.");
            Console.Write("[Enter] > ");
            Console.ReadLine();

            await endpointInstance.Send("ShoppingService", new CheckoutBasket()
            {
                UserId = userId,
            });

            OrderPlacedRecived.WaitOne();

            Console.WriteLine($"Please accept our terms and conditions.");
            Console.Write("[y|n] > ");
            var acceptTerms = Console.ReadLine() == "y";

            if (acceptTerms)
            {
                await endpointInstance.Publish(new TermsAccepted()
                {
                    OrderId = OrderId,
                });

                Console.WriteLine($"Press enter to confirm payment details.");
                Console.Write("[Enter] > ");
                Console.ReadLine();

                await endpointInstance.Publish(new PaymentDetailsProvided()
                {
                    OrderId = OrderId,
                });

                Console.WriteLine($"Please wait, we are processing your order.");
            }
            else
            {
                await endpointInstance.Publish(new TermsRejected()
                {
                    OrderId = OrderId,
                });
            }
            
            OrderAcceptedOrRejectedReceived.WaitOne();

            Console.WriteLine($"Thank you, your order was " + (HasOrderBeenAccepted ? "accepted" : "rejected") );
            Console.WriteLine("Press any key to finish");

            Console.ReadLine();
        }

       
    }
}
