namespace ACC.Common.Messaging
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeEvent<TEvent>(string @namespace) where TEvent : IEvent;

        IBusSubscriber SubscribeCommand<TCommand>(string @namespace) where TCommand : ICommand;
    }
}