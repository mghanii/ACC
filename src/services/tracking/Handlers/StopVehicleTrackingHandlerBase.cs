using ACC.Common.Exceptions;
using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Services.Tracking.Events;
using ACC.Services.Tracking.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Handlers
{
    public class StopVehicleTrackingHandlerBase
    {
        private readonly ITrackedVehicleRepository _trackedVehicleRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;

        public StopVehicleTrackingHandlerBase(ITrackedVehicleRepository trackedVehicleRepository,
            IEventBus eventBus,
            ILogger<StopVehicleTrackingHandlerBase> logger)
        {
            _trackedVehicleRepository = trackedVehicleRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task StopVehicleTracking(string vehicleId)
        {
            var trackedVehicle = await _trackedVehicleRepository.GetAsync(vehicleId)
                                  .AnyContext();

            if (trackedVehicle == null)
            {
                throw new AccException("tracked_vehicle_not_found", $"Tracked vehicle: '{vehicleId}' was not found");
            }

            await _trackedVehicleRepository.DeleteAsync(trackedVehicle.Id)
                  .AnyContext();

            await _eventBus.PublishAsync(new VehicleTrackingEndedEvent(trackedVehicle.Id))
                .AnyContext();
        }
    }
}