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
        [Fact]
        public void Constructor_PasswordEnviromentVariableShouldExist()
        {
            var password = Environment.GetEnvironmentVariable("CryptoPortfolioEmailPassword");
            Assert.NotNull(password);
        }
    }
}
