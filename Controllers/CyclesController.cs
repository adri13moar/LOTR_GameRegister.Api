using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Services.Interfaces;

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
            try
            {
                var cycles = await _cycleService.GetAllAsync();
                return Ok(cycles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a cycle by id.
        /// </summary>
        /// <param name="id">Cycle identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cycle = await _cycleService.GetByIdAsync(id);

                if (cycle == null)
                {
                    return NotFound($"Cycle with ID {id} not found.");
                }

                return Ok(cycle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
