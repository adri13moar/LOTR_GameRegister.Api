using LOTR_GameRegister.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using LOTR_GameRegister.Application.Models;
using LOTR_GameRegister.Application.Services.Interfaces;
using LOTR_GameRegister.Application.Models.Dto;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Handles user authentication: login and registration.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    [EnableRateLimiting("auth_limiter")]
    public class AuthenticationController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Authenticates a user and returns their information and a JWT token.
        /// </summary>
        /// <param name="loginDto">User credentials.</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var result = await userService.LoginAsync(loginDto);

            if (result == null)
            {
                return Unauthorized(new { message = Localizer.Get("InvalidCredentials") });
            }

            return Ok(result);
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registrationDto">User registration data.</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            var result = await userService.RegisterAsync(registrationDto);

            if (result != RegisterResult.Success)
            {
                return BadRequest(new { message = Localizer.Get("UsernameOrEmailTaken") });
            }

            return Ok(new { message = Localizer.Get("UserRegistered") });
        }
    }
}
