using ACC.Common.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Pipe.Middleware;
using System;

namespace ACC.Messaging.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly IServiceScopeFactory _scopeFactory;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = (IServiceProvider)app.ApplicationServices.GetService(typeof(IServiceProvider));
            _busClient = (IBusClient)_serviceProvider.GetService(typeof(IBusClient));
            _rabbitMqOptions = (RabbitMqOptions)_serviceProvider.GetService(typeof(RabbitMqOptions));
            _scopeFactory = _serviceProvider.GetService<IServiceScopeFactory>();
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string @namespace)
            where TCommand : ICommand
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var handler = (ICommandHandler<TCommand>)scope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));

                _busClient.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                                ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TCommand>(@namespace))));
            }

            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>(string @namespace)
            where TEvent : IEvent
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var handler = (IEventHandler<TEvent>)scope.ServiceProvider.GetRequiredService(typeof(IEventHandler<TEvent>));

                _busClient.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                    ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TEvent>(@namespace))));
            }

            return this;
        }

        private string GetQueueName<T>(string @namespace = null)
        {
            if (@namespace == null)
            {
                @namespace = _rabbitMqOptions.Namespace;
            }

            return $"{@namespace}_{typeof(T).Name}";
        }
    }
}