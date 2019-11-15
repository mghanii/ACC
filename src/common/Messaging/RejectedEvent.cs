namespace ACC.Common.Messaging
{
    public class RejectedEvent : IEvent
    {
        public string Code { get; }
        public string Message { get; }

        public RejectedEvent(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}