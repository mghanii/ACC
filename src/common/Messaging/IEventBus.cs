using System.Threading.Tasks;

namespace ACC.Common.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : IMessage;

        Task SubscribeEventAsync<TEvent>(IEventHandler<TEvent> handler) where TEvent : IEvent;

        Task SubscribeCommandAsync<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;
    }
}