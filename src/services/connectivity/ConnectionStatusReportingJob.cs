using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Services.VehicleConnectivity.Events;
using ACC.Services.VehicleConnectivity.Random;
using ACC.Services.VehicleConnectivity.Repositories;
using System;
using System.Threading.Tasks;

namespace ACC.Services.VehicleConnectivity
{
    public class ConnectionStatusReportingJob : IConnectionStatusReportingJob
    {
        private static readonly RandomGenerator _random = new RandomGenerator();

        private readonly IVehicleRepository _repository;
        private readonly IEventBus _eventBus;

        public ConnectionStatusReportingJob(IVehicleRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task ExecuteAsync()
        {
            while (true)
            {
                var vehicles = await _repository.GetAllVehicles()
                    .AnyContext();

                foreach (var vehicle in vehicles)
                {
                    var connected = _random.RandBool();

                    if (!connected) continue; // disconnected vehicle doesn't report its status

                    var @event = new VehicleStatusReportedEvent(vehicle.Id, connected);

                    await _eventBus.PublishAsync(@event)
                         .AnyContext();
                }

                await Task.Delay(TimeSpan.FromMinutes(1))
                    .AnyContext();
            }
        }
    }
}