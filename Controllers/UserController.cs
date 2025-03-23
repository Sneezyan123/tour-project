using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourProject.Infrastructure.Filters;
using TourProject.Infrastructure.ModelBinders;
using TourProject.Models;
using TourProject.Models.DTOs;
using TourProject.Services;

namespace TourProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserDto userDto)
        {
            var user = await _userService.Register(userDto);
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginPasswordUserDto userDto)
        {
            var user = await _userService.Login(userDto);
            return user;
        }
    }
}
