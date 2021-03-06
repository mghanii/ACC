﻿using ACC.Common.Messaging;
using ACC.Services.Tracking.Domain;
using Newtonsoft.Json;

namespace ACC.Services.Tracking.Events
{
    public class VehicleStatusChangedEvent : IEvent
    {
        public string VehicleId { get; }
        public string Status { get; }

        [JsonConstructor]
        public VehicleStatusChangedEvent(string vehicleId, string status)
        {
            VehicleId = vehicleId;
            Status = status;
        }
    }
}