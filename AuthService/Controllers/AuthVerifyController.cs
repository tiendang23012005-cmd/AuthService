using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthVerifyController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Verify()
        {
            return Ok(new
            {
                message = "Token hợp lệ",
                userName = User.Identity?.Name,
                idUser = User.FindFirst(
                    System.Security.Claims.ClaimTypes.NameIdentifier
                )?.Value
            });
        }
    }
}