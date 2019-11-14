using ACC.Common.Messaging;

namespace ACC.Services.Tracking.Events
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