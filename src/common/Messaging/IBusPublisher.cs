using System.Threading.Tasks;

namespace ACC.Common.Messaging
{
    public interface IBusPublisher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;
    }
}