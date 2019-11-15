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

        public async Task StopVehicleTracking(string vehicleId, bool rejectIfNotFound = true)
        {
            var exists = await _trackedVehicleRepository.ExistsAsync(vehicleId)
                                  .AnyContext();

            if (!exists)
            {
                if (rejectIfNotFound)
                {
                    var msg = $"Tracked vehicle: '{vehicleId}' was not found";
                    await _eventBus.PublishAsync(new StopVehicleTrackingRejectedEvent(vehicleId, "tracked_vehicle_not_found", msg))
                                .AnyContext();
                }
                return;
            }

            await _trackedVehicleRepository.DeleteAsync(vehicleId)
                  .AnyContext();

            await _eventBus.PublishAsync(new VehicleTrackingStoppedEvent(vehicleId))
                .AnyContext();
        }
    }
}