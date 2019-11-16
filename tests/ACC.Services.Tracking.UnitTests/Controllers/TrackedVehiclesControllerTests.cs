using ACC.Services.Tracking.Controllers;
using ACC.Services.Tracking.Domain;
using ACC.Services.Tracking.Dto;
using ACC.Services.Tracking.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.UnitTests.Controllers
{
    public class TrackedVehiclesControllerTests : TestBase
    {
        private TrackedVehiclesController _controller;
        private ITrackedVehiclesQueries _queries;
        private ILogger<TrackedVehiclesController> _logger;
        private IEnumerable<TrackedVehicleDto> _dtos;

        [OneTimeSetUp]
        public void SetUp()
        {
            _queries = Substitute.For<ITrackedVehiclesQueries>();
            _logger = Substitute.For<ILogger<TrackedVehiclesController>>();
            _controller = new TrackedVehiclesController(_queries, _logger);

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

            _queries
                  .GetAsync(query)
                  .Returns(e => _dtos.Where(x => x.Status == ((GetTrackedVehiclesQuery)e[0]).Status));

            // Act
            var actionResult = await _controller.Get(query);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<TrackedVehicleDto>>>(actionResult);
            CollectionAssert.AreEqual(expected, okResult.Value as IEnumerable<TrackedVehicleDto>);
        }

        [Test]
        public async Task Get_Tracked_Vehicles_With_Specified_Customer_Success()
        {
            // Arrange
            var query = new GetTrackedVehiclesQuery
            {
                CustomerId = TestData[0].CustomerId
            };

            var expected = _dtos.Where(x => x.CustomerId == TestData[0].CustomerId);

            _queries
                  .GetAsync(query)
                  .Returns(e => _dtos.Where(x => x.CustomerId == ((GetTrackedVehiclesQuery)e[0]).CustomerId));

            // Act
            var actionResult = await _controller.Get(query);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<TrackedVehicleDto>>>(actionResult);
            CollectionAssert.AreEqual(expected, okResult.Value as IEnumerable<TrackedVehicleDto>);
        }

        [Test]
        public async Task Get_Tracked_Vehicles_With_Specified_Status_And_Customer_Success()
        {
            // Arrange
            var query = new GetTrackedVehiclesQuery
            {
                CustomerId = TestData[0].CustomerId,
                Status = TrackedVehicleStatus.Connected
            };

            var expected = _dtos.Where(x => x.CustomerId == TestData[0].CustomerId
                                         && x.Status == TrackedVehicleStatus.Connected);

            _queries
                  .GetAsync(query)
                  .Returns(e => _dtos.Where(x => x.CustomerId == ((GetTrackedVehiclesQuery)e[0]).CustomerId
                                              && x.Status == ((GetTrackedVehiclesQuery)e[0]).Status));

            // Act
            var actionResult = await _controller.Get(query);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<TrackedVehicleDto>>>(actionResult);
            CollectionAssert.AreEqual(expected, okResult.Value as IEnumerable<TrackedVehicleDto>);
        }

        [Test]
        public async Task Get_All_Tracked_Vehicles_Success()
        {
            // Arrange
            var query = new GetTrackedVehiclesQuery();

            var expected = _dtos;

            _queries
                  .GetAsync(query)
                  .Returns(e => _dtos);

            // Act
            var actionResult = await _controller.Get(query);
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<TrackedVehicleDto>>>(actionResult);
            CollectionAssert.AreEqual(expected, okResult.Value as IEnumerable<TrackedVehicleDto>);
        }
    }
}