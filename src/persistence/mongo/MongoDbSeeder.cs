using ACC.Common.Extensions;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.Persistence.Mongo
{
    public class MongoDbSeeder : IMongoDbSeeder
    {
        protected readonly IMongoDatabase Database;

        public MongoDbSeeder(IMongoDatabase database)
        {
            Database = database;
        }

        public async Task SeedAsync()
        {
            await CustomSeedAsync()
                .AnyContext();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}