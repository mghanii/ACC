using ACC.Common.Messaging;

namespace ACC.Services.Customers.Events
{
    public class CustomerUpdatedEvent : IEvent
    {
        public string Id { get; }
        public string Name { get; }
        public string Address { get; }

        public CustomerUpdatedEvent(string id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}