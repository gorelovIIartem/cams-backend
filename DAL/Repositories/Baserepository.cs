using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BaseRepository<Tentity, Tkey> : IRepository<Tentity, Tkey>
        where Tentity : class
    {
        protected readonly DbContext context;
        protected readonly DbSet<Tentity> entity;

        public BaseRepository(DbContext _context)
        {
            context = _context;
            entity = context.Set<Tentity>();
        }

        public virtual async Task<bool> CheckIfExist(Tkey key)
        {
            return !(await context.Set<Tentity>().FindAsync(key) == null);

        }

        public virtual void Create(Tentity item)
        {
            entity.Add(item);
        }

        public virtual void Delete(Tentity item)
        {
            entity.Remove(item);
        }

        public virtual void DeleteSeveral(ICollection<Tentity> itemsCollection)
        {
            foreach (var item in itemsCollection)
                Delete(item);
        }

        public virtual IQueryable<Tentity> GetAll()
        {
            return entity;
        }

        public virtual async Task<ICollection<Tentity>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public virtual IQueryable<Tentity> GetAllIncluding(params Expression<Func<Tentity, object>>[] includeProperties)
        {
            var queryable = GetAll();
            foreach (var includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);
            return queryable;
        }

        public virtual async Task<ICollection<Tentity>> GetAllIncludingAsync(params Expression<Func<Tentity, object>>[] includeProperties)
        {
            return await GetAllIncluding(includeProperties).ToListAsync();
        }

        public virtual async Task<Tentity> GetByIdAsync(Tkey key)
        {
            return await context.Set<Tentity>().FindAsync(key);
        }

        public virtual async Task<Tentity> GetByMacAsync(Tkey key)
        {
            return await context.Set<Tentity>().FindAsync(key);
        }

        public virtual IQueryable<Tentity> GetWhere(Expression<Func<Tentity, bool>> expression)
        {
            return entity.Where(expression);
        }

        public virtual async Task<ICollection<Tentity>> GetWhereAsync(Expression<Func<Tentity, bool>> expression)
        {
            return await GetWhere(expression).ToListAsync();
        }

        public virtual void Update(Tentity item)
        {
            entity.Update(item);
        }
    }
}