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
        [Theory]
        [InlineData("CryptoPortfolioEmailPassword")]
        [InlineData("CryptoPortfolioEmailUsername")]
        public void Constructor_PasswordEnviromentVariableShouldExist(string enviromentVariableName)
        {
            var actual = Environment.GetEnvironmentVariable(enviromentVariableName);
            Assert.NotNull(actual);
        }

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
