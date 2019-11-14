using ACC.Common.Messaging;
using ACC.Services.Tracking.Domain;

namespace ACC.Services.Tracking.Events
{
    public class VehicleStatusChangedEvent : IEvent
    {
        public string VehicleId { get; }
        public TrackedVehicleStatus Status { get; }

        public VehicleStatusChangedEvent(string vehicleId, TrackedVehicleStatus status)
        {
            VehicleId = vehicleId;
            Status = status;
        }
    }
}