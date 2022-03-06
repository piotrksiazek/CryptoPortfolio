using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : IOwnedByUser
    {
        protected readonly AppDbContext Context;

        public RepositoryBase(AppDbContext context)
        {
            Context = context;
        }

        public async Task<T?> Get(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetForUser(string appUserId, int id)
        {
            return await Context.Set<T>().Where(x => x.AppUserId == appUserId && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllForUser(string appUserId)
        {
            return await Context.Set<T>().Where(x => x.AppUserId == appUserId).ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }
    }
}
