using ACC.Common.Messaging;

namespace ACC.Services.Vehicles.Events
{
    public class VehicleAddedEvent : IEvent
    {
        public string Id { get; }
        public string RegistrationNumber { get; }
        public string CustomerId { get; }

        public VehicleAddedEvent(string id, string registrationNumber, string customerId)
        {
            Id = id;
            RegistrationNumber = registrationNumber;
            CustomerId = customerId;
        }
    }
}