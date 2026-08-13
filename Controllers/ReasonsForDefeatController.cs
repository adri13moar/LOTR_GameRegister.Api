using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Endpoints to retrieve reasons for defeat used to categorize lost games.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReasonsForDefeatController(IReasonForDefeatService reasonService) : ControllerBase
    {
        private readonly IReasonForDefeatService _reasonService = reasonService;

        /// <summary>
        /// Retrieves all reasons for defeat.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reasons = await _reasonService.GetAllAsync();
            return Ok(reasons);
        }

        /// <summary>
        /// Retrieves a reason for defeat by id.
        /// </summary>
        /// <param name="id">Identifier of the reason for defeat.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reason = await _reasonService.GetByIdAsync(id);

            if (reason == null)
            {
                return NotFound(Localizer.Get("ReasonForDefeatNotFound", id));
            }

            return Ok(reason);
        }
    }
}
