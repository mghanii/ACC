using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Tracking.Commands
{
    public class TrackVehicleCommand : ICommand
    {
        public string VehicleId { get; }

        [JsonConstructor]
        public TrackVehicleCommand(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}