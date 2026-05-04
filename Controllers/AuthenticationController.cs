using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Services.Interfaces;
using LOTR_GameRegister.Api.Models.Dto;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IUserService userService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var result = await userService.LoginAsync(loginDto);

            if (result == null)
            {
                return Unauthorized(new { message = "User or Password is incorrect." });
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            var result = await userService.RegisterAsync(registrationDto);

            if (!result)
            {
                return BadRequest(new { message = "The username or the name already exists." });
            }

            return Ok(new { message = "User register succesfully!" });
        }
    }
}