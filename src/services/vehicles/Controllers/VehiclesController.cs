using ACC.Common.Extensions;
using ACC.Services.Vehicles.Dto;
using ACC.Services.Vehicles.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ACC.Services.Vehicles.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleQueries _vehicleQueries;
        private readonly ILogger _logger;

        public VehiclesController(IVehicleQueries vehicleQueries, ILogger<VehiclesController> logger)
        {
            _vehicleQueries = vehicleQueries ?? throw new ArgumentNullException(nameof(vehicleQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _vehicleQueries.GetAsync(id)
                .AnyContext();

            if (dto == null)
            {
                _logger.LogInformation($"Vehicle '{id}' was not found");
                return NotFound();
            }

            return Ok(dto);
        }
    }
}