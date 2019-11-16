using ACC.Common.Messaging;
using ACC.Services.Tracking.Commands;
using ACC.Services.Tracking.Events;
using ACC.Services.Tracking.Handlers;
using ACC.Services.Tracking.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.UnitTests.Handlers
{
    [TestFixture]
    public class StopVehicleTrackingHandlerTests
    {
        private IEventBus _eventBus;
        private ILogger<StopVehicleTrackingHandler> _logger;
        private ITrackedVehicleRepository _trackedVehicleRepository;
        private StopVehicleTrackingHandler _stopVehicleTrackingHandler;
        private string _vehicleId;
        private StopVehicleTrackingCommand _command;

        [OneTimeSetUp]
        public void Setup()
        {
            _trackedVehicleRepository = Substitute.For<ITrackedVehicleRepository>();
            _eventBus = Substitute.For<IEventBus>();
            _logger = Substitute.For<ILogger<StopVehicleTrackingHandler>>();
            _stopVehicleTrackingHandler = new StopVehicleTrackingHandler(_trackedVehicleRepository, _eventBus, _logger);
            _vehicleId = Guid.NewGuid().ToString();
            _command = new StopVehicleTrackingCommand(_vehicleId);
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Tracked_Tracked_Vehicle_Removed()
        {
            // Arrange
            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(true);

            // Act
            await _stopVehicleTrackingHandler.HandleAsync(_command);

            // Assert

            await _trackedVehicleRepository
                .Received()
                .DeleteAsync(_vehicleId);
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Tracked_Vehicle_Tracking_Stopped_Event_Published()
        {
            // Arrange
            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(true);

            // Act
            await _stopVehicleTrackingHandler.HandleAsync(_command);

            // Assert
            await _eventBus
             .Received()
             .PublishAsync(Arg.Is<VehicleTrackingStoppedEvent>(e => e.VehicleId == _vehicleId));
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Not_Tracked_Rejection_Event_Published()
        {
            // Arrange
            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(false);

            // Act
            await _stopVehicleTrackingHandler.HandleAsync(_command);

            // Assert
            await _eventBus
             .Received()
             .PublishAsync(Arg.Is<StopVehicleTrackingRejectedEvent>(e => e.VehicleId == _vehicleId
                            && e.Code == "tracked_vehicle_not_found"));
        }
    }
}