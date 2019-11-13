using System.Threading.Tasks;

namespace ACC.Common.Messaging
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}