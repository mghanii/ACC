using ACC.Common.Messaging;
using ACC.Services.Tracking.Commands;
using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Dto;
using ACC.Services.Tracking.Events;
using ACC.Services.Tracking.Handlers;
using ACC.Services.Tracking.Repositories;
using ACC.Services.Tracking.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.UnitTests.Handlers
{
    public class TrackVehicleHandlerTests
    {
        private IEventBus _eventBus;
        private ILogger<TrackVehicleHandler> _logger;
        private ITrackedVehicleRepository _trackedVehicleRepository;
        private ICustomerService _customerService;
        private IVehicleService _vehicleService;
        private TrackVehicleHandler _trackVehicleHandler;
        private string _vehicleId;
        private string _ipAddress;
        private TrackVehicleCommand _command;

        [SetUp]
        public void Setup()
        {
            _trackedVehicleRepository = Substitute.For<ITrackedVehicleRepository>();
            _customerService = Substitute.For<ICustomerService>();
            _vehicleService = Substitute.For<IVehicleService>();
            _eventBus = Substitute.For<IEventBus>();
            _logger = Substitute.For<ILogger<TrackVehicleHandler>>();
            _trackVehicleHandler = new TrackVehicleHandler(_trackedVehicleRepository, _customerService, _vehicleService, _eventBus, _logger);
            _vehicleId = Guid.NewGuid().ToString();
            _ipAddress = "216.3.128.12";
            _command = new TrackVehicleCommand(_vehicleId, _ipAddress);
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Already_Tracked_Track_Rejection_Event_Published()
        {
            // Arrange
            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(true);

            // Act
            await _trackVehicleHandler.HandleAsync(_command);

            // Assert
            await _eventBus
                .Received()
                .PublishAsync(Arg.Is<TrackVehicleRejectedEvent>(e => e.VehicleId == _vehicleId && e.Code == "vehicle_already_tracked"));
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Not_Found_Track_Rejection_Event_Published()
        {
            // Arrange
            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(false);

            _vehicleService
             .GetAsync(_vehicleId)
             .Returns((VehicleDto)null);

            // Act
            await _trackVehicleHandler.HandleAsync(_command);

            // Assert
            await _eventBus
                .Received()
                .PublishAsync(Arg.Is<TrackVehicleRejectedEvent>(e => e.VehicleId == _vehicleId && e.Code == "vehicle_not_found"));
        }

        [Test]
        public async Task HandleAsync_If_Customer_Is_Not_Found_Track_Rejection_Event_Published()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();

            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(false);

            _vehicleService
             .GetAsync(_vehicleId)
             .Returns(new VehicleDto(_vehicleId, "regNr", customerId));

            _customerService
             .GetAsync(customerId)
             .Returns((CustomerDto)null);

            // Act
            await _trackVehicleHandler.HandleAsync(_command);

            // Assert
            await _eventBus
                .Received()
                .PublishAsync(Arg.Is<TrackVehicleRejectedEvent>(e => e.VehicleId == _vehicleId
                                                                  && e.CustomerId == customerId
                                                                  && e.Code == "customer_not_found"));
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Not_Already_Tracked_Tracked_Vehicle_Added()
        {
            // Arrange
            var customer = GetTestCustomer();
            var vehicle = new VehicleDto(_vehicleId, "regNr", customer.Id);

            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(false);

            _vehicleService
             .GetAsync(_vehicleId)
             .Returns(vehicle);

            _customerService
             .GetAsync(customer.Id)
             .Returns(customer);

            // Act
            await _trackVehicleHandler.HandleAsync(_command);

            // Assert
            await _trackedVehicleRepository
                .Received()
                .AddAsync(Arg.Is<TrackedVehicle>(e => e.Id == _vehicleId
                                                   && e.CustomerId == customer.Id
                                                   && e.RegNr == vehicle.RegNr
                                                   && e.Status == TrackedVehicleStatus.Disconnected
                                                   && e.CustomerAddress == customer.Address));
        }

        [Test]
        public async Task HandleAsync_If_Vehicle_Is_Not_Already_Tracked_Vehicle_Tracked_Event_Published()
        {
            // Arrange
            var customer = GetTestCustomer();
            var vehicle = new VehicleDto(_vehicleId, "regNr", customer.Id);

            _trackedVehicleRepository
                .ExistsAsync(_vehicleId)
                .Returns(false);

            _vehicleService
             .GetAsync(_vehicleId)
             .Returns(vehicle);

            _customerService
             .GetAsync(customer.Id)
             .Returns(customer);

            // Act
            await _trackVehicleHandler.HandleAsync(_command);

            // Assert
            await _eventBus
                     .Received()
                     .PublishAsync(Arg.Is<VehicleTrackedEvent>(e => e.VehicleId == _vehicleId));
        }

        private CustomerDto GetTestCustomer()
            => new CustomerDto { Id = Guid.NewGuid().ToString(), Name = "Mohamed", Email = "Mohamed@test.test", Address = "Cairo, Egypt" };
    }
}