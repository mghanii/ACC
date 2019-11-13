using ACC.Common.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ACC.Persistence.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration, string configSectionKey)
        {
            services.AddSingleton<MongoClient>(ctx =>
            {
                var options = configuration.GetValue<MongoDbOptions>(configSectionKey);

                return new MongoClient(options.ConnectionString);
            });
            services.AddScoped<IMongoDatabase>(ctx =>
            {
                var options = ctx.GetService<MongoDbOptions>();
                var client = ctx.GetService<MongoClient>();

                return client.GetDatabase(options.Database);
            });
            services.AddScoped<IDbInitializer, MongoDbInitializer>();
            services.AddScoped<IMongoDbSeeder, MongoDbSeeder>();
        }
    }
}