using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ACC.ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }
    }
}