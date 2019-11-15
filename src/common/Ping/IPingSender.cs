using System.Threading.Tasks;

namespace ACC.Common.Ping
{
    public interface IPingSender
    {
        Task<PingReply> SendAsync(string hostNameOrAddress, int timeoutInMilliseconds);
    }
}