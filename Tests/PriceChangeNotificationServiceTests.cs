using Core.Entities;
using Infrastructure.Services;
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
        public void IsNotifiable_Test(bool greaterThanOrEqual, decimal pricePoint, decimal actualPrice, bool expected)
        {
            //arrange
            var service = new PriceChangeNotificationService();
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
