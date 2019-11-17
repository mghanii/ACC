using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Services.Tracking.Events;
using ACC.Services.Tracking.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Handlers
{
    public class VehicleDeletedHandler : StopVehicleTrackingHandlerBase, IEventHandler<VehicleDeletedEvent>

    {
        public VehicleDeletedHandler(ITrackedVehicleRepository trackedVehicleRepository,
            IBusPublisher busPublisher,
            ILogger<VehicleDeletedHandler> logger)
            : base(trackedVehicleRepository, busPublisher, logger)
        {
        }

        public async Task HandleAsync(VehicleDeletedEvent command)
        {
            await StopVehicleTracking(command.VehicleId, false)
                 .AnyContext();
        }
    }
}