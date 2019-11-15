using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Tracking.Commands
{
    public class TrackVehicleCommand : ICommand
    {
        public string VehicleId { get; }

        public string IPAddress { get; }

        [JsonConstructor]
        public TrackVehicleCommand(string vehicleId, string ipAddress)
        {
            VehicleId = vehicleId;
            IPAddress = ipAddress;
        }
    }
}