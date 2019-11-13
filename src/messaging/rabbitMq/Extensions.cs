using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;

namespace ACC.Messaging.RabbitMq
{
    public static class Extensions
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration, string configSectionKey)
        {
            var options = configuration.GetValue<RabbitMqOptions>(configSectionKey);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });

            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}