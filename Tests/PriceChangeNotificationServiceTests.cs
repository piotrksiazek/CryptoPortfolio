using Core.Entities;
using Core.Interfaces.Services;
using Infrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Core.Enums;

namespace Tests
{
    public class PriceChangeNotificationServiceTests
    {
        [Theory]
        [InlineData(true, 420, 500, NotificationType.Above)]
        [InlineData(true, 420, 300, NotificationType.None)]
        [InlineData(false, 420, 300, NotificationType.Below)]
        [InlineData(false, 420, 500, NotificationType.None)]
        [InlineData(true, 420, 420, NotificationType.Above)]
        [InlineData(false, 420, 420, NotificationType.Below)]
        public void IsNotifiable_Test(bool greaterThanOrEqual, decimal pricePoint, decimal actualPrice, NotificationType expected)
        {
            //arrange
            var mailingServiceMock = new Mock<IMailingService>();
            var service = new PriceChangeNotificationService(mailingServiceMock.Object);
            var notification = new Notification()
            {
                GreaterThanOrEqual = greaterThanOrEqual,
                PricePoint = pricePoint
            };

            //act
            NotificationType result = service.GetNotificationType(notification, actualPrice);

            //assert
            Assert.Equal(expected, result);
        }
    }
}
