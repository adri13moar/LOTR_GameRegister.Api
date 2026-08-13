using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Application.Services.Interfaces;
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
            var difficulties = await _difficultyService.GetAllAsync();
            return Ok(difficulties);
        }

        /// <summary>
        /// Retrieves a difficulty by id.
        /// </summary>
        /// <param name="id">Difficulty identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var quest = await _difficultyService.GetByIdAsync(id);

            if (quest == null)
            {
                return NotFound(Localizer.Get("DifficultyNotFound", id));
            }

            return Ok(quest);
        }
    }
}
