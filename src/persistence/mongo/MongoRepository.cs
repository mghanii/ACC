using ACC.Common.Extensions;
using ACC.Common.Repository;
using ACC.Common.Types;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACC.Persistence.Mongo
{
    public class MongoRepository<TEntity> : IRepository<TEntity, string>
        where TEntity : class, IIdentifiable
    {
        protected IMongoCollection<TEntity> Collection { get; }

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task<TEntity> GetAsync(string id)
        {
            return await GetSingleOrDefaultAsync(e => e.Id == id)
                  .AnyContext();
        }

        public async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate)
                .SingleOrDefaultAsync()
                .AnyContext();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate)
                        .ToListAsync()
                        .AnyContext();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity)
                .AnyContext();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity)
                .AnyContext();
        }

        public async Task DeleteAsync(string id)
        {
            await Collection.DeleteOneAsync(e => e.Id == id)
                .AnyContext();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate)
                 .AnyAsync()
                 .AnyContext();
        }
    }
}