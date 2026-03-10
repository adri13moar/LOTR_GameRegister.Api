using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Endpoints to retrieve difficulty levels used by quests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DifficultiesController(IDifficultyService difficultyService) : ControllerBase
    {
        private readonly IDifficultyService _difficultyService = difficultyService;

        /// <summary>
        /// Retrieves all difficulties.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var difficulties = await _difficultyService.GetAllAsync();
                return Ok(difficulties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a difficulty by id.
        /// </summary>
        /// <param name="id">Difficulty identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var quest = await _difficultyService.GetByIdAsync(id);

                if (quest == null)
                {
                    return NotFound($"Quest with ID {id} not found.");
                }

                return Ok(quest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
