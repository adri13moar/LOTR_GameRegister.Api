using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Application.Services.Interfaces;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to manage users.
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
    }
}
