using CineVerse.DTOs;
using CineVerse.models;
using CineVerse.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CineVerse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthService _authService;

        public AccountController(UserManager<IdentityUser> userManager, AuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user=new IdentityUser { UserName=registerDto.Email, Email=registerDto.Email };
            var result=await _userManager.CreateAsync(user,registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var token = _authService.GenerateJwtToken(user);


            return Ok(new {Token=token, Message = "Account registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized(new { Message = "Invalid email or password!" });
            }

            var token = _authService.GenerateJwtToken(user);

            return Ok(new { Token = token, Message = "Logged in successfully!" });
        }
    }
}