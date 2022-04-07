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

        public NotificationType GetNotificationType(Notification notification, double price)
        {
            var greaterThanOrEqual = notification.GreaterThanOrEqual;
            var pricePoint = (double)notification.PricePoint;

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

        public void Notify(Notification notification, double price, IUnitOfWork uow, NotificationType notificationType)
        {
            var notificationEmail = GetMessage(notification, price, notificationType);
            _mailingService.SendEmailAsync(notificationEmail, notification, uow);
        }

        public async Task NotifyAll(IUnitOfWork uow)
        {
            var notifications = await uow.NotificationRepository.GetAll();
            var cryptoNames = await uow.NotificationRepository.GetDistinctCryptocurrencyNames();

            Dictionary<string, double> cryptoPrices = new();

            foreach(var cryptoName in cryptoNames)
            {
                cryptoPrices[cryptoName] = await _cryptoApiCallerService.GetCryptoPrice(cryptoName);
            }


            foreach (var notification in notifications)
            {
                var price = cryptoPrices[notification.Cryptocurrency.Name];
                var notificationType = GetNotificationType(notification, price);
                if (notificationType == NotificationType.None) continue;

                Notify(notification, price, uow, notificationType);
            }
        }

        private NotificationEmail GetMessage(Notification notification, double price, NotificationType notificationType)
        {
            var email = new NotificationEmail();

            var body = $"{notification.Cryptocurrency.Name} is now {Enum.GetName(notificationType)} {notification.PricePoint} at current price {price}!";
            var title = $"{notification.Cryptocurrency.Name} notification.";

            email.Title = title;
            email.Body = body;
            email.Sender = Environment.GetEnvironmentVariable("CryptoPortfolioEmailUsername");
            if(notification.AppUser != null)
                email.Reciever = notification.AppUser.Email;

            return email;
        }
    }
}
