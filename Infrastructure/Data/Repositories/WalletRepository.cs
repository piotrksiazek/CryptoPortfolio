using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class WalletRepository : RepositoryBase<Wallet>, IWalletRepository
    {
        private readonly AppDbContext _context;

        public WalletRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public bool HasUserCryptocurrencyWithId(int cryptoId, string appUserId)
        {
            bool hasWalletWithCrypto = _context.Wallets.Any(x => x.CryptocurrencyId == cryptoId && x.AppUserId == appUserId);
            return hasWalletWithCrypto;
        }
    }
}
