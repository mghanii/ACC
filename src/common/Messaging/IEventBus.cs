using System.Threading.Tasks;

namespace ACC.Common.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;

        IEventBus SubscribeEvent<TEvent>(string @namespace) where TEvent : IEvent;

        IEventBus SubscribeCommand<TCommand>(string @namespace) where TCommand : ICommand;
    }
}