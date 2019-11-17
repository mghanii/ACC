using ACC.Common.Types;

namespace ACC.Services.Tracking.Domain
{
    public class TrackingHistory : EntityBase, IIdentifiable
    {
        public string VehicleId { get; private set; }
        public string Status { get; private set; }

        public TrackingHistory(string id, string vehicleId, string status)
            : base(id)
        {
            VehicleId = vehicleId;
            Status = status;
        }
    }
}