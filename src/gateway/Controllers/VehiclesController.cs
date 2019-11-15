using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ACC.ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;

        public VehiclesController(ILogger<VehiclesController> logger)
        {
            _logger = logger;
        }
    }
}