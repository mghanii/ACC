using ACC.Common.Messaging;

namespace ACC.ApiGateway.Events
{
    public class VehicleStatusChangedEvent : IEvent
    {
        public string VehicleId { get; }
        public string Status { get; }

        public VehicleStatusChangedEvent(string vehicleId, string status)
        {
            VehicleId = vehicleId;
            Status = status;
        }
    }
}