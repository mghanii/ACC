using ACC.Common.Types;

namespace ACC.Services.Tracking.Domain
{
    public class TrackingHistory : EntityBase, IIdentifiable
    {
        public string VehicleId { get; }
        public string Status { get; }

        public TrackingHistory(string id, string vehicleId, string status)
            : base(id)
        {
            VehicleId = vehicleId;
            Status = status;
        }
    }
}