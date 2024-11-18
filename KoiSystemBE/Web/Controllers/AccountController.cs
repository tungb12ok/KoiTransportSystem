using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserDto registerDto)
        {
            try
            {
                string token = await _userService.RegisterAsync(registerDto);
                return Ok(new { message = "User registered successfully.", data = new { token = token } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto loginDto)
        {
            try
            {
                var token = _userService.Login(loginDto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("update-profile/{userId}")]
        public async Task<IActionResult> UpdateUserProfileAsync(int userId, [FromBody] UpdateUserProfileDto updateProfileDto)
        {
            var response = await _userService.UpdateUserProfileAsync(userId, updateProfileDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfileAsync(int userId)
        {
            var response = await _userService.GetProfile(userId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("myProfile")]
        public async Task<IActionResult> GetMyProfileAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _userService.GetProfile(int.Parse(userId));
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
