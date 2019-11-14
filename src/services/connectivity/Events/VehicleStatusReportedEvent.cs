﻿using ACC.Common.Messaging;

namespace ACC.Services.VehicleConnectivity.Events
{
    public class VehicleStatusReportedEvent : IEvent
    {
        public string VehicleId { get; }
        public bool IsConnected { get; }

        public VehicleStatusReportedEvent(string vehicleId, bool isConnected)
        {
            VehicleId = vehicleId;
            IsConnected = isConnected;
        }
    }
}