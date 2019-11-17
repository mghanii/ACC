using ACC.Common.Extensions;
using ACC.Services.Tracking.Dto;
using ACC.Services.Tracking.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Controllers
{
    [ApiController]
    [Route("tracked")]
    public class TrackedVehiclesController : ControllerBase
    {
        private readonly ITrackedVehiclesQueries _queries;
        private readonly ILogger<TrackedVehiclesController> _logger;

        public TrackedVehiclesController(ITrackedVehiclesQueries queries, ILogger<TrackedVehiclesController> logger)
        {
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("vehicles")]
        [ProducesResponseType(typeof(IEnumerable<TrackedVehicleDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TrackedVehicleDto>>> Get(string customerId, string status)
        {
            var query = new GetTrackedVehiclesQuery
            {
                CustomerId = customerId,
                Status = status
            };

            var results = await _queries.GetAsync(query)
                .AnyContext();

            return Ok(results);
        }
    }
}