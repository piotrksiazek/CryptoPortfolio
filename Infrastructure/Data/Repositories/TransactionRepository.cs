using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public bool HasUserCryptocurrencyWithId(int cryptoId, string appUserId)
        {
            bool hasCryptocurrency = _context.Transactions.Any(x => x.CryptocurrencyId == cryptoId && x.AppUserId == appUserId);
            return hasCryptocurrency;
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactions(string appUserId)
        {
            return await Find(x => x.AppUserId == appUserId);
        }
    }
}
