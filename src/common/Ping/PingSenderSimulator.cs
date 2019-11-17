using ACC.Common.Extensions;
using ACC.Common.Random;
using System.Threading.Tasks;

namespace ACC.Common.Ping
{
    public class PingSenderSimulator : IPingSender
    {
        private static readonly RandomGenerator _random = new RandomGenerator();

        public async Task<PingReply> SendAsync(string hostNameOrAddress, int millisecondsTimeout)
        {
            var roundtripTime = _random.RandNonNegative(millisecondsTimeout);

            var ipStatus = _random.RandEnum<IPStatus>();

            if (ipStatus == IPStatus.TimedOut)
            {
                roundtripTime = millisecondsTimeout;
            }

            await Task.Delay(roundtripTime)
                .AnyContext();

            return new PingReply(hostNameOrAddress, ipStatus, roundtripTime);
        }
    }
}