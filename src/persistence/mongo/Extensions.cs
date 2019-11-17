using ACC.Common.Repository;
using ACC.Common.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ACC.Persistence.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration, string configSectionKey)
        {
            services.AddSingleton(context =>
            {
                var options = new MongoDbOptions();
                configuration.GetSection(configSectionKey).Bind(options);
                return options;
            });

            services.AddSingleton<MongoClient>(ctx =>
            {
                var options = ctx.GetService<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            });
            services.AddScoped<IMongoDatabase>(ctx =>
            {
                var options = ctx.GetService<MongoDbOptions>();
                var client = ctx.GetService<MongoClient>();

                return client.GetDatabase(options.Database);
            });

            services.AddScoped<IMongoDbSeeder, MongoDbSeeder>();
            services.AddScoped<IDbInitializer, MongoDbInitializer>();
        }

        public static void AddMongoRepository<TEntity>(this IServiceCollection services, string collectionName)
            where TEntity : class, IIdentifiable
        {
            services.AddScoped<IRepository<TEntity, string>>(ctx => new MongoRepository<TEntity>(ctx.GetService<IMongoDatabase>(), collectionName));
        }
    }
}