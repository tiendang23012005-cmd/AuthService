using AuthService.Data;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(
            AuthDbContext context,
            JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    message = "Username và password không được để trống"
                });
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.UserName == request.UserName &&
                    x.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Sai username hoặc password"
                });
            }

            var token = _jwtService.GenerateToken(user);

            // Lưu token để phù hợp yêu cầu bài thực hành
            user.Token = token;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Đăng nhập thành công",
                idUser = user.IdUser,
                userName = user.UserName,
                token = token
            });
        }
    }
}