using Core.Entities;
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

        public PriceChangeNotificationService(IMailingService mailingService)
        {
            _mailingService = mailingService;
        }

        public bool IsNotifiable(Notification notification, decimal price)
        {
            var greaterThanOrEqual = notification.GreaterThanOrEqual;
            var pricePoint = notification.PricePoint;

            if(greaterThanOrEqual)
            {
                if(price >= pricePoint)
                {
                    return true;
                }
            }
            else
            {
                if(price <= pricePoint)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
