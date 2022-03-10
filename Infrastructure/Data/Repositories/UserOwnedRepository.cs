using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserOwnedRepository<T> : RepositoryBase<T>, IUserOwnedRepository<T> where T : IOwnedByUser
    {
        public UserOwnedRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<T?> GetForUser(string appUserId, int id)
        {
            return await Context.Set<T>().Where(x => x.AppUserId == appUserId && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllForUser(string appUserId)
        {
            return await Context.Set<T>().Where(x => x.AppUserId == appUserId).ToListAsync();
        }
    }
}
