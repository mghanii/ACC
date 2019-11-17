using Microsoft.AspNetCore.Mvc;

namespace ACC.ApiGateway.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index() => Ok("Api gateway Service");
    }
}