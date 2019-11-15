using ACC.Common.Extensions;
using ACC.Common.Messaging;
using ACC.Common.Ping;
using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Events;
using ACC.Services.Tracking.Options;
using ACC.Services.Tracking.Repositories;
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
        private readonly ITrackedVehicleRepository _trackedVehicleRepository;
        private readonly ITrackingHistoryRepository _trackingHistoryRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<VehiclesTrackingService> _logger;
        private readonly IPingSender _pingSender;
        private readonly IOptions<TrackingOptions> _options;

        public VehiclesTrackingService(ITrackedVehicleRepository trackedVehicleRepository,
            ITrackingHistoryRepository trackingHistoryRepository,
            IEventBus eventBus,
            ILogger<VehiclesTrackingService> logger,
            IPingSender pingSender,
            IOptions<TrackingOptions> options)
        {
            _trackedVehicleRepository = trackedVehicleRepository;
            _trackingHistoryRepository = trackingHistoryRepository;
            _eventBus = eventBus;
            _logger = logger;
            _pingSender = pingSender;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"Vechicles tracking service is starting.");

            stoppingToken.Register(() => _logger.LogDebug($"Vehicles tracking service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                // fire and forget?
                await CheckVehiclesConnectivityAsync()
                     .AnyContext();

                await Task.Delay(_options.Value.PingTimeFrame, stoppingToken);
            }

            _logger.LogDebug($"Vehicles tracking service is stopping.");
        }

        public async Task CheckVehiclesConnectivityAsync()
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

                await _eventBus.PublishAsync(@event)
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