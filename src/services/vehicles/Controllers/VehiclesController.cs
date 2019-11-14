using ACC.Common.Extensions;
using ACC.Services.Vehicles.Queries;
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
        public async Task<IActionResult> Get(string id)
        {
            var dto = await _vehicleQueries.GetAsync(id)
                .AnyContext();

            return Ok(dto);
        }
    }
}