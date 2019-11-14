using ACC.Common.Messaging;

namespace ACC.Services.Vehicles.Events
{
    public class VehicleDeletedEvent : IEvent
    {
        public string VehicleId { get; }

        public VehicleDeletedEvent(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}