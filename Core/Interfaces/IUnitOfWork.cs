using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository TransactionRepository { get; }
        IBalanceRepository BalanceRepository { get; }
        IWalletRepository WalletRepository { get; }
        Task<int> SaveAsync();
    }
}
