using ACC.ApiGateway.Commands;
using ACC.ApiGateway.Dto;
using ACC.ApiGateway.Queries;
using ACC.ApiGateway.Services;
using ACC.Common.Extensions;
using ACC.Common.Messaging;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AnyPolicy")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<TrackingController> _logger;

        public TrackingController(ITrackingService trackingService,
            IBusPublisher busPublisher,
            ILogger<TrackingController> logger)
        {
            _trackingService = trackingService;
            _busPublisher = busPublisher;
            _logger = logger;
        }

        [HttpGet("vehicles")]
        [ProducesResponseType(typeof(IEnumerable<TrackedVehicleDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TrackedVehicleDto>>> GetTrackedVehicles(string customerId, string status)
        {
            var query = new GetTrackedVehiclesQuery
            {
                CustomerId = customerId,
                Status = status
            };

            var results = await _trackingService.GetTrackedVehiclesAsync(query)
                .AnyContext();

            return Ok(results);
        }

        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> TrackVehicle([FromBody] TrackVehicleCommand command)
        {
            if (command == null
                || string.IsNullOrEmpty(command.VehicleId)
                || string.IsNullOrEmpty(command.IPAddress))
            {
                return BadRequest();
            }

            await _busPublisher.PublishAsync(command)
                .AnyContext();

            return Accepted();
        }

        [HttpPost("stop")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> StopVehicleTracking([FromBody] StopVehicleTrackingCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.VehicleId))
            {
                return BadRequest();
            }

            await _busPublisher.PublishAsync(command)
                .AnyContext();

            return Accepted();
        }
    }
}