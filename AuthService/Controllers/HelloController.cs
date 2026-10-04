using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/hello")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Hello()
        {
            return Ok(new
            {
                message = "Hello World",
                userName = User.Identity?.Name
            });
        }
    }
}