using ibop.Api.DTOs;
using ibop.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ibop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_userService.GetAll());

        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            var user = _userService.Create(dto);
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }

        [HttpPut("{id}/activate")]
        public IActionResult Activate(int id)
        {
            _userService.Activate(id);
            return NoContent();
        }

        [HttpPut("{id}/deactivate")]
        public IActionResult Deactivate(int id)
        {
            _userService.Deactivate(id);
            return NoContent();
        }
    }

}
