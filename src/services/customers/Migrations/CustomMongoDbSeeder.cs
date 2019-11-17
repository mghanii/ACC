using ACC.Common.Extensions;
using ACC.Persistence.Mongo;
using ACC.Services.Customers.Domain;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.Services.Customers.Migrations
{
    public class CustomMongoDbSeeder : MongoDbSeeder
    {
        public CustomMongoDbSeeder(IMongoDatabase database)
            : base(database)
        {
        }

        protected override async Task CustomSeedAsync()
        {
            var cursor = await Database.ListCollectionsAsync()
                .AnyContext();

            var collections = await cursor.ToListAsync()
                .AnyContext();

            if (!collections.Any())
            {
                var collection = Database.GetCollection<Customer>("customers");

                foreach (var item in _seedData)
                {
                    await collection.InsertOneAsync(item);
                }
            }
        }

        private readonly Customer[] _seedData = new[] {
                     new Customer("kallesgrustransporter","Kalles Grustransporter AB"),
                     new Customer("johansbulk","Johans Bulk AB"),
                     new Customer("haraldsvardetransporter","Haralds Värdetransporter AB"),
                   };
    }
}