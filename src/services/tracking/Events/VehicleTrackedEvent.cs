using ACC.Common.Messaging;

namespace ACC.Services.Tracking.Events
{
    public class VehicleTrackedEvent : IEvent
    {
        public string VehicleId { get; }

        public VehicleTrackedEvent(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}