using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Repositories.Implementations;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Endpoints to retrieve reasons for defeat used to categorize lost games.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReasonsForDefeatController(ReasonForDefeatRepository reasonRepository) : ControllerBase
    {
        private readonly ReasonForDefeatRepository _reasonRepository = reasonRepository;

        /// <summary>
        /// Retrieves all reasons for defeat.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reasons = await _reasonRepository.GetAllAsync();
                return Ok(reasons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a reason for defeat by id.
        /// </summary>
        /// <param name="id">Identifier of the reason for defeat.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var reason = await _reasonRepository.GetByIdAsync(id);

                if (reason == null)
                {
                    return NotFound($"Reason for defeat with ID {id} not found.");
                }

                return Ok(reason);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}