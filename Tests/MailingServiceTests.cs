using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class MailingServiceTests
    {
        //private readonly IUnitOfWork _uow;

        //public MailingServiceTests(IUnitOfWork uow)
        //{
        //    _uow = uow;
        //}
        [Theory]
        [InlineData("CryptoPortfolioEmailPassword")]
        [InlineData("CryptoPortfolioEmailUsername")]
        public void Constructor_PasswordEnviromentVariableShouldExist(string enviromentVariableName)
        {
            var actual = Environment.GetEnvironmentVariable(enviromentVariableName);
            Assert.NotNull(actual);
        }

        //[Fact]
        //public void SendEmailAsync_ShouldSucceed()
        //{
        //    //arrange
        //    var mailingService = new MailingService();
        //    bool didSend = false;
        //    mailingService.HandleSuccessfulEmailDelegate = () => SuccesfulEmailReplacment(ref didSend);
        //    mailingService.HandleFailedEmailDelegate = () => FailedEmailReplacment(ref didSend);

        //    var uowMock = new Mock<IUnitOfWork>();

        //    var email = new NotificationEmail();
        //    email.Title = "title";
        //    email.Body = "body";
        //    email.Sender = Environment.GetEnvironmentVariable("CryptoPortfolioEmailUsername");
        //    email.Reciever = Environment.GetEnvironmentVariable("CryptoPortfolioEmailUsername");

        //    var notification = new Notification();

        //    //act
        //    mailingService.SendEmailAsync(email, notification, _uow);
        //    Thread.Sleep(3000); //should be enough for email to be sent.

        //    Assert.True(didSend);

        //}

        private void SuccesfulEmailReplacment(ref bool didSend)
        {
            didSend = true;
        }

        private void FailedEmailReplacment(ref bool didSend)
        {
            didSend = false;
        }
    }
}
