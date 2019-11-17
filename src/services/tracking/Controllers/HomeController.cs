using Microsoft.AspNetCore.Mvc;

namespace ACC.Services.Tracking.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => Ok("Tracking Service");
    }
}