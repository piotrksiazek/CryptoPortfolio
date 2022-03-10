using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITransactionRepository : IUserOwnedRepository<Transaction>, IHaveThisCrypto
    {
        Task<IEnumerable<Transaction>> GetUserTransactions(string appUserId);
    }
}
