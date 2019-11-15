using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Tracking.Events
{
    public class TrackVehicleRejectedEvent : RejectedEvent
    {
        public string VehicleId { get; }
        public string CustomerId { get; }

        [JsonConstructor]
        public TrackVehicleRejectedEvent(string vehicleId, string customerId, string code, string message)
            : base(code, message)
        {
            VehicleId = vehicleId;
            CustomerId = customerId;
        }
    }
}