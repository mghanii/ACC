using ACC.Common.Exceptions;
using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Services.Vehicles.Commands;
using ACC.Services.Vehicles.Events;
using ACC.Services.Vehicles.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Handlers
{
    public class DeleteVehicleHandler : ICommandHandler<DeleteVehicleCommand>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger _logger;

        public DeleteVehicleHandler(IVehicleRepository vehicleRepository,
            IBusPublisher busPublisher,
            ILogger<AddVehicleHandler> logger)
        {
            _vehicleRepository = vehicleRepository;
            _busPublisher = busPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteVehicleCommand command)
        {
            var vehicle = await _vehicleRepository.GetAsync(command.VehicleId)
                             .AnyContext();

            if (vehicle == null)
            {
                throw new AccException("vehicle_not_found", $"Vehicle: '{command.VehicleId}' was not found");
            }

            vehicle.SetDeleteFlag();

            await _vehicleRepository.UpdateAsync(vehicle)
                              .AnyContext();

            await _busPublisher.PublishAsync(new VehicleDeletedEvent(vehicle.Id))
                .AnyContext();
        }
    }
}