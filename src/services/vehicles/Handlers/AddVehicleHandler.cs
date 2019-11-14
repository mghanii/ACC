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
    public class AddVehicleHandler : ICommandHandler<AddVehicleCommand>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerService _customerService;
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;

        public AddVehicleHandler(IVehicleRepository vehicleRepository,
            ICustomerService customerService,
            IEventBus eventBus,
            ILogger<AddVehicleHandler> logger)
        {
            _vehicleRepository = vehicleRepository;
            _customerService = customerService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task HandleAsync(AddVehicleCommand command)
        {
            var customer = await _customerService.GetAsync(command.CustomerId)
                             .AnyContext();

            if (customer == null)
            {
                throw new AccException("customer_not_found", $"Customer: '{command.CustomerId}' was not found");
            }

            var vehicle = new Vehicle(command.Id,
                        command.RegNr,
                        command.Color,
                        command.Brand,
                        command.Model,
                        command.Description,
                        command.CustomerId,
                        customer.Name);

            await _vehicleRepository.AddAsync(vehicle)
                              .AnyContext();

            var @event = new VehicleAddedEvent(vehicle.Id, vehicle.RegNr, vehicle.CustomerId);

            await _eventBus.PublishAsync(@event)
                .AnyContext();
        }
    }
}