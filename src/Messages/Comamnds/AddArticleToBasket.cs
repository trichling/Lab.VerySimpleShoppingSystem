using System;
using NServiceBus;

namespace Messages.Comamnds
{
    public class AddArticleToBasket : ICommand
    {
        public Guid UserId { get; set; }
        public string ArticleNumber { get; set; }
        public int Quantity { get; set; }
        
    }
}