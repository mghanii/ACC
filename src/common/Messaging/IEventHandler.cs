using System.Threading.Tasks;

namespace ACC.Common.Messaging
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}