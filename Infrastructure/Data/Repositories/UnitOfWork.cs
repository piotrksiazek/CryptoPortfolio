using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, ITransactionRepository transactionRepository, IBalanceRepository balanceRepository,
            IWalletRepository walletRepository, ICryptoCurrencyRepository cryptoCurrencyRepository, INotificationRepository notificationRepository)
        {
            _context = context;
            TransactionRepository = transactionRepository;
            BalanceRepository = balanceRepository;
            WalletRepository = walletRepository;
            CryptoCurrencyRepository = cryptoCurrencyRepository;
            NotificationRepository = notificationRepository;
        }

        public ITransactionRepository TransactionRepository { get; private set; }
        public IBalanceRepository BalanceRepository { get; }
        public IWalletRepository WalletRepository { get; }
        public ICryptoCurrencyRepository CryptoCurrencyRepository { get; }
        public INotificationRepository NotificationRepository { get; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
