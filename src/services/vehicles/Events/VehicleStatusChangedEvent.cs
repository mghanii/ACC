using ACC.Common.Messaging;

namespace ACC.Services.Vehicles.Events
{
    public class VehicleStatusChangedEvent : IEvent
    {
        public string VehicleId { get; }
        public bool IsConnected { get; }

        public VehicleStatusChangedEvent(string vehicleId, bool isConnected)
        {
            VehicleId = vehicleId;
            IsConnected = isConnected;
        }
    }
}