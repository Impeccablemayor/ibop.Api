using ibop.Api.DTOs;
using ibop.Api.Services;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ibop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // optional: all endpoints require login by default
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // Only Admins can see all users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll() => Ok(_userService.GetAllAsync());

        // Only Admins can create new users
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var user = await _userService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = user.Id },
                user
            );
        }

        // Only Admins can activate users
        [HttpPut("{id}/activate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(int id)
        {
            await _userService.ActivateAsync(id);
            return NoContent();
        }

        // Only Admins can deactivate users
        [HttpPut("{id}/deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _userService.DeactivateAsync(id);
            return NoContent();
        }
    }
}
