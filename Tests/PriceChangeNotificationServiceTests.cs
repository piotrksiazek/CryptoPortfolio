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
using Core.Interfaces;
using Xunit.Abstractions;

namespace Tests
{
    public class PriceChangeNotificationServiceTests
    {
        private readonly ITestOutputHelper output;
        private static readonly Cryptocurrency _bitcoin = new Cryptocurrency()
        {
            Id = 1,
            Name = "bitcoin"
        };

        private static readonly Cryptocurrency _ethereum = new Cryptocurrency()
        {
            Id = 2,
            Name = "ethereum"
        };

        private static readonly Cryptocurrency _usdt = new Cryptocurrency()
        {
            Id = 3,
            Name = "usdt"
        };

        public PriceChangeNotificationServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData(true, 420, 500, NotificationType.Above)]
        [InlineData(true, 420, 300, NotificationType.None)]
        [InlineData(false, 420, 300, NotificationType.Below)]
        [InlineData(false, 420, 500, NotificationType.None)]
        [InlineData(true, 420, 420, NotificationType.Above)]
        [InlineData(false, 420, 420, NotificationType.Below)]
        public void GetNotificationType_Test(bool greaterThanOrEqual, double pricePoint, double actualPrice, NotificationType expected)
        {
            //arrange
            var mailingServiceMock = new Mock<IMailingService>();
            var cryptoApiCallerService = new Mock<ICryptoApiCallerService>();
            var service = new PriceChangeNotificationService(mailingServiceMock.Object, cryptoApiCallerService.Object);
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

        [Theory]
        [MemberData(nameof(Data))]
        public async Task NotifAll_Test(List<Notification> notifications, int expectedSendEmailAsyncCalls)
        {
            //arrange
            var uowMock = new Mock<IUnitOfWork>();


            uowMock.Setup(x => x.NotificationRepository.GetAll()).ReturnsAsync(notifications);
            uowMock.Setup(x => x.NotificationRepository.GetDistinctCryptocurrencyNames()).ReturnsAsync(GetSampleNames());

            var cryptoApiCallerSercviceFake = new CryptoApiCallerServiceImplementation();

            var mailingServiceMock = new Mock<MailingServiceImplementation>();
            mailingServiceMock.As<IMailingService>();

            var notificationService = new PriceChangeNotificationService(mailingServiceMock.Object, cryptoApiCallerSercviceFake);

            //act
            await notificationService.NotifyAll(uowMock.Object);

            //assert
            mailingServiceMock.Verify(x => x.SendEmailAsync(
                It.IsAny<NotificationEmail>(), It.IsAny<Notification>(), It.IsAny<IUnitOfWork>(), null, null), Times.Exactly(expectedSendEmailAsyncCalls));
        }

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { PricesEqualToPricePoints_ShouldNotify_3_times(), 3 };
            yield return new object[] { PricesHigherThanPricePoints_ShouldNotify_3_times(), 3 };
            yield return new object[] { PricesLowerThanPricePoints_ShouldNotify_0_times(), 0 };
            yield return new object[] { SomePricesLowerThanPricePoints_ShouldNotify_1_times(), 1 };
        }
        private static List<Notification> PricesEqualToPricePoints_ShouldNotify_3_times()
        {
            List<Notification> notificationList = new()
            {
                new()
                {
                    PricePoint = 30000,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 1,
                    Cryptocurrency = _bitcoin
                },
                new()
                {
                    PricePoint = 3000,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 2,
                    Cryptocurrency = _ethereum
                },
                new()
                {
                    PricePoint = 3,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 3,
                    Cryptocurrency = _usdt
                }
            };

            return notificationList;
        }

        private static List<Notification> PricesHigherThanPricePoints_ShouldNotify_3_times()
        {
            List<Notification> notificationList = new()
            {
                new()
                {
                    PricePoint = 29999.123,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 1,
                    Cryptocurrency = _bitcoin
                },
                new()
                {
                    PricePoint = 2999.321,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 2,
                    Cryptocurrency = _ethereum
                },
                new()
                {
                    PricePoint = 2.99,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 3,
                    Cryptocurrency = _usdt
                }
            };

            return notificationList;
        }

        private static List<Notification> PricesLowerThanPricePoints_ShouldNotify_0_times()
        {
            List<Notification> notificationList = new()
            {
                new()
                {
                    PricePoint = 31000,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 1,
                    Cryptocurrency = _bitcoin
                },
                new()
                {
                    PricePoint = 3100,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 2,
                    Cryptocurrency = _ethereum
                },
                new()
                {
                    PricePoint = 31,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 3,
                    Cryptocurrency = _usdt
                }
            };

            return notificationList;
        }

        private static List<Notification> SomePricesLowerThanPricePoints_ShouldNotify_1_times()
        {
            List<Notification> notificationList = new()
            {
                new()
                {
                    PricePoint = 31000,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 1,
                    Cryptocurrency = _bitcoin
                },
                new()
                {
                    PricePoint = 3100,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 2,
                    Cryptocurrency = _ethereum
                },
                new()
                {
                    PricePoint = 2,
                    GreaterThanOrEqual = true,
                    CryptocurrencyId = 3,
                    Cryptocurrency = _usdt
                }
            };

            return notificationList;
        }

        private List<string> GetSampleNames()
        {
            var names = new List<string>();

            foreach(var notification in PricesEqualToPricePoints_ShouldNotify_3_times())
            {
                names.Add(notification.Cryptocurrency.Name);
            }
            return names;
        }

        public class CryptoApiCallerServiceImplementation : ICryptoApiCallerService
        {
            public virtual Task<CryptocurrencyData?> GetCryptoData(string name)
            {
                throw new NotImplementedException();
            }

            public virtual async Task<double> GetCryptoPrice(string cryptoName)
            {
                var prices = new Dictionary<string, double>();
                prices["bitcoin"] = 30000;
                prices["ethereum"] = 3000;
                prices["usdt"] = 3;
                return prices[cryptoName];
            }
        }

        public class MailingServiceImplementation : IMailingService
        {
            public virtual void SendEmailAsync(NotificationEmail email, Notification notification, IUnitOfWork unitOfWork, Action handleSuccesfulEmailMethod = null, Action handleFailedEmailMethod = null)
            {
                
            }

            public void HandleSuccessfulEmail(Notification notification, IUnitOfWork uow)
            {
                throw new NotImplementedException();
            }

            public void HandleFailedEmail()
            {
                throw new NotImplementedException();
            }
        }
    }
}
