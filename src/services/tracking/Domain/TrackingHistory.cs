using ACC.Common.Types;

namespace ACC.Services.Tracking.Domain
{
    public class TrackingHistory : EntityBase, IIdentifiable
    {
        public string VehicleId { get; }
        public TrackedVehicleStatus Status { get; }

        public TrackingHistory(string id, string vehicleId, TrackedVehicleStatus status)
            : base(id)
        {
            VehicleId = vehicleId;
            Status = status;
        }
    }
}