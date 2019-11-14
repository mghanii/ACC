using ACC.Common.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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