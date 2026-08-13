using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to list available cycles (expansion sets) in the game.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CyclesController(ICycleService cycleService) : ControllerBase
    {
        private readonly ICycleService _cycleService = cycleService;

        /// <summary>
        /// Retrieves all cycles.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cycles = await _cycleService.GetAllAsync();
            return Ok(cycles);
        }

        /// <summary>
        /// Retrieves a cycle by id.
        /// </summary>
        /// <param name="id">Cycle identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cycle = await _cycleService.GetByIdAsync(id);

            if (cycle == null)
            {
                return NotFound(Localizer.Get("CycleNotFound", id));
            }

            return Ok(cycle);
        }
    }
}
