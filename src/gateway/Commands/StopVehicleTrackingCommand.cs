using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.ApiGateway.Commands
{
    public class StopVehicleTrackingCommand : ICommand
    {
        public string VehicleId { get; }

        [JsonConstructor]
        public StopVehicleTrackingCommand(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}