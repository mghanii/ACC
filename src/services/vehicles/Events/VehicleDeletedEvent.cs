using ACC.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Events
{
    public class VehicleDeletedEvent : IEvent
    {
        public string VehicleId { get; }

        public VehicleDeletedEvent(string vehicleId)
        {
            VehicleId = vehicleId;
        }
    }
}