using Xunit;
using Moq;
using Core.Interfaces;
using Infrastructure.Services;
using Core.Entities;
using System.Threading.Tasks;

namespace Tests
{
    public class BalanceServiceTests
    {
        [Theory]
        [InlineData(true, true, false)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        public void BalanceServiceTest(bool hasTransactionWithCrypto, bool hasWalletWithCrypto, bool expected)
        {
            //arrange
            var transaction = new Transaction
            {
                CryptocurrencyId = 1

            };

            var UoWMock = new Mock<IUnitOfWork>();
            UoWMock.Setup(x => x.TransactionRepository.HasUserCryptocurrencyWithId(It.IsAny<int>(), It.IsAny<string>())).Returns(hasTransactionWithCrypto);
            UoWMock.Setup(x => x.WalletRepository.HasUserCryptocurrencyWithId(It.IsAny<int>(), It.IsAny<string>())).Returns(hasWalletWithCrypto);
            UoWMock.Setup(x => x.BalanceRepository.GetForUserWithCryptoId(It.IsAny<string>(), transaction.CryptocurrencyId)).ReturnsAsync(new Balance());

            var balanceService = new BalanceService(UoWMock.Object);
            //act
            var actual = balanceService.HandleRebalance(transaction, It.IsAny<string>()).Result;

            //assert
            Assert.Equal(expected, actual);
            
        }
    }
}
