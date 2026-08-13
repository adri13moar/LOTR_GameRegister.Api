using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var quests = await _questService.GetAllAsync();
            return Ok(quests);
        }

        /// <summary>
        /// Retrieves a quest by id.
        /// </summary>
        /// <param name="id">Quest identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var quest = await _questService.GetByIdAsync(id);

            if (quest == null)
            {
                return NotFound(Localizer.Get("QuestNotFound", id));
            }

            return Ok(quest);
        }
    }
}
