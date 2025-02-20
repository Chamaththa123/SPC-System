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

            if (token == null)
                return Unauthorized("Invalid credentials");

            if (token == "User not active")
                return Unauthorized(new { message = "Your account is not active. Please contact support." });

            var user = await _userService.GetUserDetailsByEmail(request.Email);
            return Ok(new { Token = token, User = user });
        }


        [HttpPut("activate-user/{userId}")]
        public async Task<IActionResult> ActivateUser(int userId)
        {
            var result = await _userService.ActivateUser(userId);

            if (!result)
                return BadRequest(new { message = "User not found or already activated" });

            return Ok(new { message = "User account activated successfully" });
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("get-user-by-id/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }
    }
}
