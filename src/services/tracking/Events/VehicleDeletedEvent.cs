using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Tracking.Events
{
    public class VehicleDeletedEvent : IEvent
    {
        public string VehicleId { get; }

        [JsonConstructor]
        public VehicleDeletedEvent(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}