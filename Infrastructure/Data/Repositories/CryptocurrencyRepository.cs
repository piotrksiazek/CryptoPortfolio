﻿using Core.Entities;
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
        private readonly AppDbContext _context;

        public CryptocurrencyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
