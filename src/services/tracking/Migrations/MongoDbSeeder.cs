using ACC.Persistence.Mongo;
using ACC.Services.Tracking.Domain;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Migrations
{
    public class CustomMongoDbSeeder : MongoDbSeeder
    {
        public CustomMongoDbSeeder(IMongoDatabase database)
            : base(database)
        {
        }

        protected override async Task CustomSeedAsync()
        {
            var collection = Database.GetCollection<TrackedVehicle>("vehicles");

            await collection.InsertManyAsync(_seedData);
        }

        private readonly TrackedVehicle[] _seedData = new[] {
                     new TrackedVehicle("YS2R4X20005399401","94.224.250.144", "ABC123","kallesgrustransporter","Kalles Grustransporter AB","Cementvägen 8, 111 11 Södertälje"),
                     new TrackedVehicle("VLUR4X20009093588","11.106.191.224", "DEF456","kallesgrustransporter","Kalles Grustransporter AB","Cementvägen 8, 111 11 Södertälje"),
                     new TrackedVehicle("VLUR4X20009048066","190.166.194.12", "GHI789","kallesgrustransporter","Kalles Grustransporter AB","Cementvägen 8, 111 11 Södertälje"),
                     new TrackedVehicle("YS2R4X20005388011","48.226.144.103", "JKL012","johansbulk","Johans Bulk AB","Balkvägen 12, 222 22 Stockholm"),
                     new TrackedVehicle("YS2R4X20005387949","145.1.95.177", "MNO345","johansbulk","Johans Bulk AB","Balkvägen 12, 222 22 Stockholm"),
                     new TrackedVehicle("VLUR4X20009048066","12.25.82.23", "PQR678","haraldsvardetransporter","Haralds Värdetransporter AB","Budgetvägen 1, 333 33 Uppsala"),
                     new TrackedVehicle("YS2R4X20005387055","208.92.39.218", "STU901","haraldsvardetransporter","Haralds Värdetransporter AB","Budgetvägen 1, 333 33 Uppsala"),
                   };
    }
}