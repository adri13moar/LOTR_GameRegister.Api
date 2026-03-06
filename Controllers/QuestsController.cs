using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Repositories.Implementations;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestsController(QuestRepository questRepository) : ControllerBase
    {
        private readonly QuestRepository _questRepository = questRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var quests = await _questRepository.GetAllAsync();
                return Ok(quests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var quest = await _questRepository.GetByIdAsync(id);

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
