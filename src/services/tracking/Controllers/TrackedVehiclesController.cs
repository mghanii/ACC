using ACC.Services.Tracking.Dto;
using ACC.Services.Tracking.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ACC.Common.Extensions;
using System.Collections.Generic;

namespace ACC.Services.Tracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackedVehiclesController : ControllerBase
    {
        private readonly ITrackedVehiclesQueries _queries;
        private readonly ILogger _logger;

        public TrackedVehiclesController(ITrackedVehiclesQueries queries, ILogger<TrackedVehiclesController> logger)
        {
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TrackedVehicleDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<TrackedVehicleDto>> Get([FromQuery]GetTrackedVehiclesQuery query)
        {
            var results = await _queries.GetAsync(query)
                .AnyContext();

            return Ok(results);
        }
    }
}