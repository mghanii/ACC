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

                foreach (var item in GetSeedData())
                {
                    await collection.InsertOneAsync(item);
                }
            }
        }

        private static Customer[] GetSeedData()
        {
            var data = new Customer[]
                  {
                     new Customer("kallesgrustransporter","Kalles Grustransporter AB"),
                     new Customer("johansbulk","Johans Bulk AB"),
                     new Customer("haraldsvardetransporter","Haralds Värdetransporter AB"),
                   };

            data[0].SetAddress("Cementvägen 8", "", "Södertälje", "", "Sweden", "111 11");
            data[1].SetAddress("Balkvägen 12", "", "Stockholm", "", "Sweden", "222 22");
            data[2].SetAddress("Budgetvägen 8", "", "Uppsala", "", "Sweden", "333 33");

            return data;
        }
    }
}