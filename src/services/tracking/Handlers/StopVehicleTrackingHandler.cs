using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Services.Tracking.Commands;
using ACC.Services.Tracking.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Handlers
{
    public class StopVehicleTrackingHandler : StopVehicleTrackingHandlerBase, ICommandHandler<StopVehicleTrackingCommand>
    {
        public StopVehicleTrackingHandler(ITrackedVehicleRepository trackedVehicleRepository,
            IBusPublisher busPublisher,
            ILogger<StopVehicleTrackingHandler> logger)
            : base(trackedVehicleRepository, busPublisher, logger)
        {
        }

        public async Task HandleAsync(StopVehicleTrackingCommand command)
        {
            await StopVehicleTracking(command.VehicleId)
                 .AnyContext();
        }
    }
}