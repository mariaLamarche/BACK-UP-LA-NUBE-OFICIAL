// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using AuthSystemBackend.Models;
using AuthSystemBackend.Services;

namespace AuthSystemBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _authService.RegisterAsync(request);
                return Ok(new { message = "Usuario registrado exitosamente", userId = user.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.LoginAsync(request);
                return Ok(new { token = token, message = "Login exitoso" });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDatabase()
        {
            try
            {
                var userCount = await _authService.GetUserCountAsync();
                return Ok(new { 
                    message = "Conexión exitosa a la base de datos", 
                    userCount = userCount,
                    timestamp = DateTime.Now 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { 
                    message = "Error de conexión a la base de datos", 
                    error = ex.Message 
                });
            }
        }
    }
}
