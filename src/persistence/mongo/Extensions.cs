﻿using ACC.Common.Repository;
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

        public static void AddMongoRepository<TEntity>(this IServiceCollection services, string collectionName)
            where TEntity : class, IIdentifiable
        {
            services.AddScoped<IRepository<TEntity, string>>(ctx => new MongoRepository<TEntity>(ctx.GetService<IMongoDatabase>(), collectionName));
        }
    }
}