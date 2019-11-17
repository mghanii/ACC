using ACC.Common.Extensions;
using ACC.Common.Messaging;
using RawRabbit;
using RawRabbit.Pipe.Middleware;
using System.Threading.Tasks;

namespace ACC.Messaging.RabbitMq
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;
        private readonly RabbitMqOptions _rabbitMqOptions;

        public BusPublisher(IBusClient client, RabbitMqOptions options)
        {
            _busClient = client;
            _rabbitMqOptions = options;
        }

        public async Task PublishAsync<TMessage>(TMessage message)
          where TMessage : IMessage
        {
            await _busClient.PublishAsync(message, ctx => ctx.UseConsumeConfiguration(cfg => cfg.FromQueue(GetQueueName<TMessage>())))
                .AnyContext();
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