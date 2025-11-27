using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ----------------------------------------------------
        // 1. REGISTER USER
        // ----------------------------------------------------
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var success = await _authService.RegisterAsync(request);

            if (!success)
                return BadRequest("Username or email already exists.");

            return Ok("User registered successfully.");
        }

        // ----------------------------------------------------
        // 2. LOGIN
        // ----------------------------------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            if (response == null)
                return Unauthorized("Invalid username or password.");

            return Ok(response);
        }

        // ----------------------------------------------------
        // 3. REFRESH TOKEN
        // ----------------------------------------------------
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var response = await _authService.RefreshTokenAsync(refreshToken);

            if (response == null)
                return Unauthorized("Invalid or expired refresh token.");

            return Ok(response);
        }
    }
}
