using Core.Entities;
using Core.Enums;
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
    }
}
