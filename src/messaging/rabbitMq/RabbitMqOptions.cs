using RawRabbit.Configuration;

namespace ACC.Messaging.RabbitMq
{
    public class RabbitMqOptions : RawRabbitConfiguration
    {
        public string Namespace { get; set; }
    }
}