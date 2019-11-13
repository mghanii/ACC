using ACC.Common.Extensions;
using ACC.Common.Messaging;
using Microsoft.AspNetCore.Builder;
using RawRabbit;
using RawRabbit.Pipe.Middleware;
using System;
using System.Threading.Tasks;

namespace ACC.Messaging.RabbitMq
{
    public class EventBus : IEventBus
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMqOptions _rabbitMqOptions;

        public EventBus(IApplicationBuilder app)
        {
            _serviceProvider = (IServiceProvider)app.ApplicationServices.GetService(typeof(IServiceProvider));
            _busClient = (IBusClient)_serviceProvider.GetService(typeof(IBusClient));
            _rabbitMqOptions = (RabbitMqOptions)_serviceProvider.GetService(typeof(RabbitMqOptions));
        }

        public async Task PublishAsync<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            await _busClient.PublishAsync(message, ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TMessage>())))
                .AnyContext();
        }

        public IEventBus SubscribeCommand<TCommand>(string @namespace)
            where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_serviceProvider
                              .GetService(typeof(ICommandHandler<TCommand>));

            _busClient.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                            ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TCommand>(@namespace))));

            return this;
        }

        public IEventBus SubscribeEvent<TEvent>(string @namespace)
            where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)_serviceProvider
                                .GetService(typeof(IEventHandler<TEvent>));

            _busClient.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TEvent>(@namespace))));

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