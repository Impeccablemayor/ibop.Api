using ibop.Api.DTOs;
using ibop.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ibop.Api.Controllers
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

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto dto)
        {
            var token = _authService.Login(dto);
            return Ok(new { token });
        }
    }

}
