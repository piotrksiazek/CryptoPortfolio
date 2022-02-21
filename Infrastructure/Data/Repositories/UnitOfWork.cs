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

        public UnitOfWork(AppDbContext context, ICryptocurrencyRepository cryptocurrencyRepository, ITransactionRepository transactionRepository)
        {
            _context = context;
            CryptocurrencyRepository = cryptocurrencyRepository;
            TransactionRepository = transactionRepository;
        }

        public ICryptocurrencyRepository CryptocurrencyRepository { get; private set; }

        public ITransactionRepository TransactionRepository { get; private set; }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
