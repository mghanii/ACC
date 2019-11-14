using ACC.Common.Messaging;
using Newtonsoft.Json;

namespace ACC.Services.Vehicles.Commands
{
    public class DeleteVehicleCommand : ICommand
    {
        public string VehicleId { get; }

        [JsonConstructor]
        public DeleteVehicleCommand(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}