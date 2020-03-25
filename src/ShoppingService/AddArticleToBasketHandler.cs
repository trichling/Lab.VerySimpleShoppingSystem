using System.Threading.Tasks;
using Messages;
using Messages.Comamnds;
using NServiceBus;
using NServiceBus.Logging;

namespace ShoppingService
{
    public class AddArticleToBasketHandler : IHandleMessages<AddArticleToBasket>
    {
        public static ILog Log = LogManager.GetLogger<AddArticleToBasketHandler>();     

        public async Task Handle(AddArticleToBasket message, IMessageHandlerContext context)
        {
            Log.Info($"Add article {message.ArticleNumber} to basket of user {message.UserId}");
            await Task.CompletedTask;
        }
    }
}