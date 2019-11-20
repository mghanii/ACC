using ACC.Common.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using Polly;
using System;

namespace ACC.Messaging.RabbitMq
{
    public static class Extensions
    {
        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app) => new BusSubscriber(app);

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration, string configSectionKey)
        {
            var retry = Policy
                       .Handle<Exception>()
                       .WaitAndRetryForever(retryAttempt => TimeSpan.FromMilliseconds(30));

            var options = new RabbitMqOptions();
            configuration.GetSection(configSectionKey).Bind(options);

            services.AddSingleton(context => options);

            retry.Execute(() =>
            {
                var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
                {
                    ClientConfiguration = options
                });

                services.AddSingleton<IBusClient>(_ => client);
                services.AddTransient<IBusPublisher, BusPublisher>();
            });
        }
    }
}