using ACC.Common.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Persistence.Mongo
{
    public class MongoDbInitializer : IMongoDbInitializer
    {
        private static bool _initialized;
        private readonly bool _seed;
        private readonly IMongoDatabase _database;
        private readonly IMongoDbSeeder _seeder;

        public MongoDbInitializer(IMongoDatabase database,
            IMongoDbSeeder seeder,
            MongoDbOptions options)
        {
            _database = database;
            _seeder = seeder;
            _seed = options.Seed;
        }

        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            RegisterConventions();
            _initialized = true;

            if (_seed)
            {
                await _seeder.SeedAsync()
                    .AnyContext();
            }
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("Conventions", new MongoDbConventions(), x => true);
        }

        private class MongoDbConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}