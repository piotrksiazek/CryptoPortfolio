using Core.Interfaces.Services;
using Infrastructure.Services;
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
        [Theory]
        [InlineData("CryptoPortfolioEmailPassword")]
        [InlineData("CryptoPortfolioEmailUsername")]
        public void Constructor_PasswordEnviromentVariableShouldExist(string enviromentVariableName)
        {
            var actual = Environment.GetEnvironmentVariable(enviromentVariableName);
            Assert.NotNull(actual);
        }
    }
}
