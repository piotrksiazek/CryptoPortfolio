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

namespace Tests
{
    public class PriceChangeNotificationServiceTests
    {
        [Theory]
        [InlineData(true, 420, 500, true)]
        [InlineData(true, 420, 300, false)]
        [InlineData(false, 420, 300, true)]
        [InlineData(false, 420, 500, false)]
        [InlineData(true, 420, 420, true)]
        [InlineData(false, 420, 420, true)]
        public void IsNotifiable_Test(bool greaterThanOrEqual, decimal pricePoint, decimal actualPrice, bool expected)
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
            bool result = service.IsNotifiable(notification, actualPrice);

            //assert
            Assert.Equal(expected, result);
        }
    }
}
