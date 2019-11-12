using ACC.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACC.Common.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : IIdentifiable<TKey>
    {
        Task<TEntity> GetAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TKey id);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}