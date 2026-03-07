using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Repositories.Implementations;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to list available cycles (expansion sets) in the game.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CyclesController(CycleRepository cycleRepository) : ControllerBase
    {
        private readonly CycleRepository _cycleRepository = cycleRepository;

        /// <summary>
        /// Retrieves all cycles.
        /// </summary>
        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var cycles = await _cycleRepository.GetAllAsync();
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
                var cycle = await _cycleRepository.GetByIdAsync(id);

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
