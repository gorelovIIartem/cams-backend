using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        void Create(TEntity item);
        void Delete(TEntity item);
        void Update(TEntity item);
        void DeleteSeveral(ICollection<TEntity> itemsCollection);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAsync(TKey key);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> expression);
        Task<ICollection<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<bool> CheckIfExist(TKey key);
    }
}