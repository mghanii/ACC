using ACC.Common.Extensions;
using ACC.Persistence.Mongo;
using ACC.Services.Vehicles.Domain;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Migrations
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
                var collection = Database.GetCollection<Vehicle>("vehicles");

                foreach (var item in _seedData)
                {
                    await collection.InsertOneAsync(item);
                }
            }
        }

        private readonly Vehicle[] _seedData = new[] {
                     new Vehicle("YS2R4X20005399401", "ABC123","Red","Ford","C-Max Hybrid","","kallesgrustransporter","Kalles Grustransporter AB"),
                     new Vehicle("VLUR4X20009093588", "DEF456","Blue","Ford","Fiesta","","kallesgrustransporter","Kalles Grustransporter AB"),
                     new Vehicle("VLUR4X20009048066", "GHI789","Blue","Ford","Focus","","kallesgrustransporter","Kalles Grustransporter AB"),
                     new Vehicle("YS2R4X20005388011", "JKL012","Black","Kia","Cadenza","","johansbulk","Johans Bulk AB"),
                     new Vehicle("YS2R4X20005387949", "MNO345","White","Kia","Forte","","johansbulk","Johans Bulk AB"),
                     new Vehicle("VLUR4X20009048044", "PQR678","Red","Volkswagen","Arteon","","haraldsvardetransporter","Haralds Värdetransporter AB"),
                     new Vehicle("YS2R4X20005387055", "STU901","Black","Volkswagen","Atlas","","haraldsvardetransporter","Haralds Värdetransporter AB"),
                   };
    }
}