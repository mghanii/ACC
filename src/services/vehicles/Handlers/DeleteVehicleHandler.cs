using ACC.Common.Messaging;
using ACC.Services.Vehicles.Commands;
using ACC.Services.Vehicles.Repositories;
using ACC.Services.Vehicles.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ACC.Common.Extensions;
using ACC.Common.Exceptions;
using ACC.Services.Vehicles.Domain;
using ACC.Services.Vehicles.Events;

namespace ACC.Services.Vehicles.Handlers
{
    public class DeleteVehicleHandler : ICommandHandler<DeleteVehicleCommand>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;

        public DeleteVehicleHandler(IVehicleRepository vehicleRepository,
            IEventBus eventBus,
            ILogger<AddVehicleHandler> logger)
        {
            _vehicleRepository = vehicleRepository;
            _eventBus = eventBus;
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

            await _eventBus.PublishAsync(new VehicleDeletedEvent(vehicle.Id))
                .AnyContext();
        }
    }
}