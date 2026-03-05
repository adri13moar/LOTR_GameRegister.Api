using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Repositories;
using LOTR_GameRegister.Api.Models;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CyclesController(CycleRepository cycleRepository) : ControllerBase
    {
        private readonly CycleRepository _cycleRepository = cycleRepository;

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
