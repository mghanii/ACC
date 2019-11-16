using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Dto;
using ACC.Services.Tracking.Queries;
using ACC.Services.Tracking.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.UnitTests.Queries
{
    public class TrackedVehiclesQueriesTests : TestBase
    {
        private TrackedVehiclesQueries _trackedVehiclesQueries;
        private ITrackedVehicleRepository _trackedVehicleRepository;
        private ILogger<TrackedVehiclesQueries> _logger;
        private IEnumerable<TrackedVehicleDto> _dtos;

        [OneTimeSetUp]
        public void SetUp()
        {
            _trackedVehicleRepository = Substitute.For<ITrackedVehicleRepository>();
            _logger = Substitute.For<ILogger<TrackedVehiclesQueries>>();
            _trackedVehiclesQueries = new TrackedVehiclesQueries(_trackedVehicleRepository, _logger);
            _trackedVehicleRepository
                 .GetAsync(Arg.Any<Expression<Func<TrackedVehicle, bool>>>())
                 .Returns(e => TestData
                             .Where(((Expression<Func<TrackedVehicle, bool>>)e[0])
                             .Compile()));

            TestData[0].SetConnectionStatus(TrackedVehicleStatus.Connected);
            TestData[2].SetConnectionStatus(TrackedVehicleStatus.Connected);
            TestData[4].SetConnectionStatus(TrackedVehicleStatus.Connected);

            _dtos = TestData.Select(v =>
            {
                return new TrackedVehicleDto(v.Id, v.RegNr, v.Status, v.CustomerId, v.CustomerName, v.CustomerAddress);
            });
        }

        [Test]
        public async Task Get_Tracked_Vehicles_With_Specified_Status_Success()
        {
            // Arrange
            var query = new GetTrackedVehiclesQuery
            {
                Status = TrackedVehicleStatus.Connected
            };

            var expected = _dtos.Where(x => x.Status == TrackedVehicleStatus.Connected);

            // Act
            var actual = await _trackedVehiclesQueries.GetAsync(query);

            // Assert
            Assert.AreEqual(3, actual.Count());
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Get_Tracked_Vehicles_With_Specified_Customer_Success()
        {
            // Arrange
            var query = new GetTrackedVehiclesQuery
            {
                CustomerId = TestData[3].CustomerId
            };

            var expected = _dtos.Where(x => x.CustomerId == TestData[3].CustomerId);

            // Act
            var actual = await _trackedVehiclesQueries.GetAsync(query);

            // Assert
            Assert.AreEqual(2, actual.Count());
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Get_Tracked_Vehicles_With_Specified_Status_And_Customer_Success()
        {
            // Arrange
            var query = new GetTrackedVehiclesQuery
            {
                CustomerId = TestData[3].CustomerId,
                Status = TrackedVehicleStatus.Connected
            };

            var expected = _dtos.Where(x => x.CustomerId == TestData[3].CustomerId
                                         && x.Status == TrackedVehicleStatus.Connected);

            // Act
            var actual = await _trackedVehiclesQueries.GetAsync(query);

            // Assert
            Assert.AreEqual(1, actual.Count());
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Get_All_Tracked_Vehicles_Success()
        {
            // Arrange
            var query = new GetTrackedVehiclesQuery();

            // Act
            var actual = await _trackedVehiclesQueries.GetAsync(query);

            // Assert
            Assert.AreEqual(TestData.Length, actual.Count());
            CollectionAssert.AreEqual(_dtos, actual);
        }
    }
}