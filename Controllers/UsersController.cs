using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Models.Dto;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to manage users and authentication.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="userRegistrationDto">User registration data.</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            try
            {
                var result = await userService.RegisterAsync(userRegistrationDto);

                if (!result)
                {
                    return BadRequest("Username or Email is already in use.");
                }

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Authenticates a user and returns their information.
        /// </summary>
        /// <param name="loginDto">User credentials.</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var authResponse = await userService.LoginAsync(loginDto);

                if (authResponse == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}