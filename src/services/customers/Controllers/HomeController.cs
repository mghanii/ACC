using Microsoft.AspNetCore.Mvc;

namespace ACC.Services.Customers.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => Ok("Customers Service");
    }
}