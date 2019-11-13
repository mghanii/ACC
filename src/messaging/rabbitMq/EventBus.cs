using ACC.Common.Extensions;
using ACC.Common.Messaging;
using RawRabbit;
using RawRabbit.Pipe.Middleware;
using System.Reflection;
using System.Threading.Tasks;

namespace ACC.Messaging.RabbitMq
{
    public class EventBus : IEventBus
    {
        private readonly IBusClient _busClient;

        public EventBus(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task PublishAsync<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            await _busClient.PublishAsync(message)
                .AnyContext();
        }

        public async Task SubscribeCommandAsync<TCommand>(ICommandHandler<TCommand> handler)
            where TCommand : ICommand
        {
            await _busClient.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                             ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TCommand>())))
                             .AnyContext();
        }

        public async Task SubscribeEventAsync<TEvent>(IEventHandler<TEvent> handler)
            where TEvent : IEvent
        {
            await _busClient.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TEvent>())))
                .AnyContext();
        }

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
    }
}