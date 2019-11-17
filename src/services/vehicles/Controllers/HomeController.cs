using Microsoft.AspNetCore.Mvc;

namespace ACC.Services.Vehicles.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => Ok("Vehicles Service");
    }
}