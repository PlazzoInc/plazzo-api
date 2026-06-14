using Microsoft.AspNetCore.Mvc;
using plazzo_api.dto.request.users;
using plazzo_api.service;

namespace plazzo_api.controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var token = await _service.RegisterAsync(request);
            return token is null
                ? Conflict(new { message = "Email already in use." })
                : Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _service.LoginAsync(request);
            return token is null
                ? Unauthorized(new { message = "Invalid credentials." })
                : Ok(new { token });
        }
    }
}