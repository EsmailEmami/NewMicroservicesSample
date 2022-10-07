using Microsoft.AspNetCore.Mvc;
using User.Application.Core.User;
using User.Application.Core.User.Dtos;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            var id = await _userService.AddUser(dto);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var token = await _userService.Login(dto);
            return Ok(token);
        }
    }
}
