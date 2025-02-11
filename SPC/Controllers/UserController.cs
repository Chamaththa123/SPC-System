using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPC.Models;
using SPC.Services;

namespace SPC.Controllers
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
        public async Task<IActionResult> Register(User user)
        {
            bool success = await _userService.RegisterUser(user);
            return success ? Ok(new { Message = "User registered successfully" }) : BadRequest("Registration failed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _userService.Login(request);
            var user = await _userService.GetUserDetailsByEmail(request.Email);
            return token == null ? Unauthorized("Invalid credentials") : Ok(new { Token = token,User =user });
        }
    }
}
