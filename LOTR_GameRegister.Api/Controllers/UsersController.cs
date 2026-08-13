using LOTR_GameRegister.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Application.Models;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Services.Interfaces;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to manage users and authentication.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController(IUserService userService) : ControllerBase
    {

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="userRegistrationDto">User registration data.</param>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var result = await userService.RegisterAsync(userRegistrationDto);

            if (result != RegisterResult.Success)
            {
                return BadRequest(Localizer.Get("UsernameOrEmailTaken"));
            }

            return Ok(Localizer.Get("UserRegistered"));
        }

        /// <summary>
        /// Authenticates a user and returns their information.
        /// </summary>
        /// <param name="loginDto">User credentials.</param>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var authResponse = await userService.LoginAsync(loginDto);

            if (authResponse == null)
            {
                return Unauthorized(Localizer.Get("InvalidCredentials"));
            }

            return Ok(authResponse);
        }
    }
}
