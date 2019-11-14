using ACC.Common.Messaging;

namespace ACC.Services.Tracking.Events
{
    public class VehicleTrackingEndedEvent : IEvent
    {
        public string VehicleId { get; }

        public VehicleTrackingEndedEvent(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}