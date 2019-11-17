using ACC.Common.Messaging;
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
    public class VehicleDeletedHandlerTests
    {
        private IBusPublisher _busPublisher;
        private ILogger<VehicleDeletedHandler> _logger;
        private ITrackedVehicleRepository _trackedVehicleRepository;
        private VehicleDeletedHandler _vehicleDeletedHandler;
        private string _vehicleId;
        private VehicleDeletedEvent _event;

        [OneTimeSetUp]
        public void Setup()
        {
            _trackedVehicleRepository = Substitute.For<ITrackedVehicleRepository>();
            _busPublisher = Substitute.For<IBusPublisher>();
            _logger = Substitute.For<ILogger<VehicleDeletedHandler>>();
            _vehicleDeletedHandler = new VehicleDeletedHandler(_trackedVehicleRepository, _busPublisher, _logger);
            _vehicleId = Guid.NewGuid().ToString();
            _event = new VehicleDeletedEvent(_vehicleId);
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Tracked_Tracked_Vehicle_Removed()
        {
            // Arrange
            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(true);

            // Act
            await _vehicleDeletedHandler.HandleAsync(_event);

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
            await _vehicleDeletedHandler.HandleAsync(_event);

            // Assert
            await _busPublisher
             .Received()
             .PublishAsync(Arg.Is<VehicleTrackingStoppedEvent>(e => e.VehicleId == _vehicleId));
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Not_Tracked_Rejection_Event_Not_Published()
        {
            // Arrange
            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(false);

            // Act
            await _vehicleDeletedHandler.HandleAsync(_event);

            // Assert
            await _busPublisher
             .DidNotReceive()
             .PublishAsync(Arg.Any<StopVehicleTrackingRejectedEvent>());
        }
    }
}