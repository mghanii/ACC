using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Tracking.Events
{
    public class VehicleTrackingStoppedEvent : IEvent
    {
        public string VehicleId { get; }

        [JsonConstructor]
        public VehicleTrackingStoppedEvent(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}