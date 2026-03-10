using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Endpoints to retrieve quest metadata used in games.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class QuestsController(IQuestService questService) : ControllerBase
    {
        private readonly IQuestService _questService = questService;

        /// <summary>
        /// Retrieves all quests.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var quests = await _questService.GetAllAsync();
                return Ok(quests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a quest by id.
        /// </summary>
        /// <param name="id">Quest identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var quest = await _questService.GetByIdAsync(id);

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
