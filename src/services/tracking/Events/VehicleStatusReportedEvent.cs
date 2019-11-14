using ACC.Common.Messaging;
using ACC.Services.Tracking.Domain;

namespace ACC.Services.Tracking.Events
{
    public class VehicleStatusReportedEvent : IEvent
    {
        public string VehicleId { get; }
        public TrackedVehicleStatus Status { get; }

        public VehicleStatusReportedEvent(string vehicleId, TrackedVehicleStatus status)
        {
            VehicleId = vehicleId;
            Status = status;
        }
    }
}