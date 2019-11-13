using MongoDB.Driver;
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
            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}