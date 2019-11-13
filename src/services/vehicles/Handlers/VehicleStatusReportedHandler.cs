using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Services.Vehicles.Domain;
using ACC.Services.Vehicles.Events;
using ACC.Services.Vehicles.Repositories;
using System;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Handlers
{
    public class VehicleStatusReportedHandler : IEventHandler<VehicleStatusReportedEvent>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleHistoryRepository _vehicleHistoryRepository;
        private readonly IEventBus _eventBus;

        public VehicleStatusReportedHandler(IVehicleRepository vehicleRepository,
            IVehicleHistoryRepository vehicleHistoryRepository,
            IEventBus eventBus)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleHistoryRepository = vehicleHistoryRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(VehicleStatusReportedEvent @event)
        {
            var vehicle = await _vehicleRepository.GetAsync(@event.VehicleId)
                                  .AnyContext();

            if (@event.IsConnected != vehicle.IsConnected)
            {
                vehicle.SetConnectionStatus(@event.IsConnected);

                await _vehicleRepository.UpdateAsync(vehicle)
                      .AnyContext();

                var history = new VehicleHistory(Guid.NewGuid().ToString(), vehicle.Id, @event.IsConnected);

                await _vehicleHistoryRepository.AddAsync(history)
                     .AnyContext();

                await _eventBus.PublishAsync(new VehicleStatusChangedEvent(vehicle.Id, @event.IsConnected))
                    .AnyContext();
            }
        }
    }
}