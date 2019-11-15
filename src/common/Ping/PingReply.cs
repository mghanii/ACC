namespace ACC.Common.Ping
{
    public class PingReply
    {
        public string IPAddress { get; }
        public IPStatus IPStatus { get; }

        /// <summary>
        /// Roundtrip time in milliseconds
        /// </summary>
        public long RoundtripTime { get; }

        public PingReply(string iPAddress, IPStatus iPStatus, long roundtripTime)
        {
            IPAddress = iPAddress;
            IPStatus = iPStatus;
            RoundtripTime = roundtripTime;
        }
    }

    public enum IPStatus
    {
        Unknown = -1,
        Success = 0,
        TimedOut = 1
    }
}