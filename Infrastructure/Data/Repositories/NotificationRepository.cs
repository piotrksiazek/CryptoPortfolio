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
    public class NotificationRepository : UserOwnedRepository<Notification>, INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<string>> GetDistinctCryptocurrencyNames()
        {
            return await _context.Notifications.Select(x => x.Cryptocurrency.Name).Distinct().ToListAsync();
        }

        public override async Task<IEnumerable<Notification>> GetAll()
        {
            var query = _context.Notifications
                .Include(a => a.AppUser)
                .Include(c => c.Cryptocurrency);

            return await query.ToListAsync();
        }
    }
}
