using ACC.Common.Exceptions;
using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Events;
using ACC.Services.Tracking.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Handlers
{
    public class VehicleStatusReportedHandler : IEventHandler<VehicleStatusReportedEvent>
    {
        private readonly ITrackedVehicleRepository _vehicleRepository;
        private readonly ITrackingHistoryRepository _vehicleHistoryRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;

        public VehicleStatusReportedHandler(ITrackedVehicleRepository vehicleRepository,
            ITrackingHistoryRepository vehicleHistoryRepository,
            IEventBus eventBus,
            ILogger<VehicleStatusReportedHandler> logger)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleHistoryRepository = vehicleHistoryRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(VehicleStatusReportedEvent @event)
        {
            var vehicle = await _vehicleRepository.GetAsync(@event.VehicleId)
                                  .AnyContext();

            if (vehicle == null)
            {
                throw new AccException("vehicle_not_found", $"Vehicle: '{@event.VehicleId}' was not found");
            }

            if (@event.Status != vehicle.Status)
            {
                vehicle.SetConnectionStatus(@event.Status);

                await _vehicleRepository.UpdateAsync(vehicle)
                      .AnyContext();

                var history = new TrackingHistory(Guid.NewGuid().ToString(), vehicle.Id, @event.Status);

                await _vehicleHistoryRepository.AddAsync(history)
                     .AnyContext();

                await _eventBus.PublishAsync(new VehicleStatusChangedEvent(vehicle.Id, @event.Status))
                    .AnyContext();
            }
        }
    }
}