using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Tracking.Events
{
    public class StopVehicleTrackingRejectedEvent : RejectedEvent
    {
        public string VehicleId { get; }

        [JsonConstructor]
        public StopVehicleTrackingRejectedEvent(string vehicleId, string code, string message)
            : base(code, message)
        {
            VehicleId = vehicleId;
        }
    }
}