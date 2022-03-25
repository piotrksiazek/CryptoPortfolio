using Core.Entities;
using Core.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class MailingServiceTests
    {
        private readonly IServiceProvider _provider;

        public MailingServiceTests(IServiceProvider provider)
        {
            _provider = provider;
        }

        [Theory]
        [InlineData("CryptoPortfolioEmailPassword")]
        [InlineData("CryptoPortfolioEmailUsername")]
        public void Constructor_PasswordEnviromentVariableShouldExist(string enviromentVariableName)
        {
            var actual = Environment.GetEnvironmentVariable(enviromentVariableName);
            Assert.NotNull(actual);
        }

        [Fact]
        public void SendEmailAsync_ShouldSucceed()
        {
            ////arrange
            //var mailingService = _provider.GetRequiredService<IMailingService>();

            var email = new NotificationEmail();
            email.Title = "title";
            email.Body = "body";
            email.Sender = Environment.GetEnvironmentVariable("CryptoPortfolioEmailUsername");
            email.Reciever = Environment.GetEnvironmentVariable("CryptoPortfolioEmailUsername");

            ////act

            //Action action = () => mailingService.SendEmailAsync(email);

            //assert

            var mailingServiceMock = new Mock<IMailingService>();
            mailingServiceMock.Setup(x => x.SendEmailAsync(email));

            mailingServiceMock.Verify(x => x.HandleSuccessfulEmail());

        }
    }
}
