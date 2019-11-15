using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Tracking.Events
{
    public class VehicleTrackedEvent : IEvent
    {
        public string VehicleId { get; }

        [JsonConstructor]
        public VehicleTrackedEvent(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}