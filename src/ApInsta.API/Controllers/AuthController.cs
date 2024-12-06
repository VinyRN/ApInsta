using ApInsta.Domain.dtos.request;
using ApInsta.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApInsta.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Endpoint para registrar um novo usuário.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var userId = await _authService.RegisterAsync(request.Name, request.Email, request.Password);
                return Ok(new { UserId = userId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint para autenticar um usuário.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Login, request.Password);

            if (token == null)
                return Unauthorized(new { Error = "Credenciais inválidas." });

            return Ok(new { Token = token });
        }
    }
}
