using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PriceChangeNotificationService : IPriceChangeNotificationService
    {
        private readonly IMailingService _mailingService;
        private readonly ICryptoApiCallerService _cryptoApiCallerService;

        public PriceChangeNotificationService(IMailingService mailingService, ICryptoApiCallerService cryptoApiCallerService)
        {
            _mailingService = mailingService;
            _cryptoApiCallerService = cryptoApiCallerService;
        }

        public NotificationType GetNotificationType(Notification notification, decimal price)
        {
            var greaterThanOrEqual = notification.GreaterThanOrEqual;
            var pricePoint = notification.PricePoint;

            if(greaterThanOrEqual)
            {
                if(price >= pricePoint)
                {
                    return NotificationType.Above;
                }
            }
            else
            {
                if(price <= pricePoint)
                {
                    return NotificationType.Below;
                }
            }
            return NotificationType.None;
        }

        public void Notify(Notification notification, decimal price, IUnitOfWork uow)
        {
            var notificationEmail = GetMessage(notification, price);
            _mailingService.SendEmailAsync(notificationEmail, notification, uow);
        }

        public async Task NotifyAll()
        {

        }

        private NotificationEmail GetMessage(Notification notification, decimal price)
        {
            var email = new NotificationEmail();

            string aboveOrBelow = notification.GreaterThanOrEqual ? "above" : "below";
            var body = $"{notification.Cryptocurrency.Name} is now {aboveOrBelow} {notification.PricePoint} at current price {price}!";
            var title = $"{notification.Cryptocurrency.Name} notification.";

            email.Title = title;
            email.Body = body;
            email.Sender = Environment.GetEnvironmentVariable("CryptoPortfolioEmailUsername");
            email.Reciever = notification.AppUser.Email;

            return email;
        }
    }
}
