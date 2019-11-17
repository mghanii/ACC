using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Common.Ping;
using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Events;
using ACC.Services.Tracking.Options;
using ACC.Services.Tracking.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Scheduling
{
    public class VehiclesTrackingService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<VehiclesTrackingService> _logger;
        private readonly IOptions<TrackingOptions> _options;
        private readonly IPingSender _pingSender;
        private ITrackedVehicleRepository _trackedVehicleRepository;
        private ITrackingHistoryRepository _trackingHistoryRepository;
        private IBusPublisher _busPublisher;

        public VehiclesTrackingService(IServiceProvider services,
            ILogger<VehiclesTrackingService> logger,
            IPingSender pingSender,
            IOptions<TrackingOptions> options)
        {
            _services = services;
            _logger = logger;
            _pingSender = pingSender;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"Vechicles tracking service is starting.");
            stoppingToken.Register(() => _logger.LogDebug($"Vehicles tracking service is stopping."));

            using (var scope = _services.CreateScope())
            {
                _trackedVehicleRepository = scope.ServiceProvider.GetRequiredService<ITrackedVehicleRepository>();
                _trackingHistoryRepository = scope.ServiceProvider.GetRequiredService<ITrackingHistoryRepository>();
                _busPublisher = scope.ServiceProvider.GetRequiredService<IBusPublisher>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    // fire and forget?
                    await CheckVehiclesConnectivityAsync()
                         .AnyContext();

                    await Task.Delay(_options.Value.PingTimeFrame, stoppingToken);
                }
            }

            _logger.LogDebug($"Vehicles tracking service is stopping.");
        }

        private async Task CheckVehiclesConnectivityAsync()
        {
            var vehicles = await _trackedVehicleRepository.GetAsync(_ => true)
                .AnyContext();

            var tasks = vehicles.Select(v => _pingSender.SendAsync(v.IPAddress, _options.Value.PingTimeout));

            var results = await Task.WhenAll(tasks)
                .AnyContext();

            foreach (var r in results)
            {
                var vehicle = vehicles.FirstOrDefault(v => v.IPAddress == r.IPAddress);

                if (vehicle == null) // vehicle is no longer tracked
                {
                    _logger.LogInformation($"Vehicle with IP address '{r.IPAddress}' was not found");
                    continue;
                }

                var status = r.IPStatus == IPStatus.Success
                    ? TrackedVehicleStatus.Connected
                    : TrackedVehicleStatus.Disconnected;

                if (status == vehicle.Status) continue;

                var @event = new VehicleStatusChangedEvent(vehicle.Id, status);

                await _busPublisher.PublishAsync(@event)
                    .AnyContext();

                vehicle.SetConnectionStatus(status);

                // Enhancement: use bulk update
                await _trackedVehicleRepository.UpdateAsync(vehicle)
                     .AnyContext();

                var history = new TrackingHistory(Guid.NewGuid().ToString(), vehicle.Id, status);

                await _trackingHistoryRepository.AddAsync(history)
                     .AnyContext();
            }
        }
    }
}