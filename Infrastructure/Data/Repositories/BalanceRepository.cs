using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BalanceRepository : RepositoryBase<Balance>, IBalanceRepository
    {
        private readonly AppDbContext _context;
        public BalanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Balance> GetForUserWithCryptoId(string appUserId, int cryptoId)
        {
            return await _context.Balances.Where(x => x.AppUserId == appUserId && x.CryptocurrencyId == cryptoId).FirstOrDefaultAsync();
        }
    }

}
