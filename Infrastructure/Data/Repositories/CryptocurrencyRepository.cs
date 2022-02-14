using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CryptocurrencyRepository : RepositoryBase<Cryptocurrency>, ICryptocurrencyRepository
    {
        public CryptocurrencyRepository(DbContext context) : base(context)
        {
        }
    }
}
