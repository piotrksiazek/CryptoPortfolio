using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BalanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleRebalance(Transaction transaction, string appUserId)
        {
            var cryptoId = transaction.CryptocurrencyId;
            bool hasTransactionWithCrypto = _unitOfWork.TransactionRepository.HasUserCryptocurrencyWithId(cryptoId, appUserId);
            bool hasWalletWithCrypto = _unitOfWork.WalletRepository.HasUserCryptocurrencyWithId(cryptoId, appUserId);

            var balanceToAdd = new Balance()
            {
                Amount = transaction.Amount,
                AppUserId = appUserId,
                CryptocurrencyId = cryptoId,
            };

            if (!(hasTransactionWithCrypto || hasWalletWithCrypto))
            {
    
                await _unitOfWork.BalanceRepository.Add(balanceToAdd);
            }
            else
            {
                var currentBalance = await _unitOfWork.BalanceRepository.GetForUserWithCryptoId(appUserId, cryptoId);
                if(currentBalance == null)
                {
                    await _unitOfWork.BalanceRepository.Add(balanceToAdd);
                }
                else
                {
                    currentBalance.Amount += transaction.Amount;
                }
            }
            await _unitOfWork.SaveAsync();
        }
    }
}
