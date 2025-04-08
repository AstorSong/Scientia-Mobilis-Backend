// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ScientiaMobilis.Models;
using ScientiaMobilis.Services;

namespace ScientiaMobilis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Registration endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Map DTO to domain model
                var user = new User
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    Password = registerDto.Password,
                    EmailVerified = false
                };

                var createdUser = await _userService.RegisterAsync(user);
                return Ok(new { userId = createdUser.Uid, message = "Registration successful" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Sign‑in endpoint
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserDto signInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var authUser = await _userService.SignInAsync(signInDto.Email, signInDto.Password);
                return Ok(new { userId = authUser.Uid, message = "Sign‑in successful" });
            }
            catch (System.Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
