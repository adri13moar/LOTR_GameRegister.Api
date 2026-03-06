using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Repositories.Implementations;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReasonsForDefeatController(ReasonForDefeatRepository reasonRepository) : ControllerBase
    {
        private readonly ReasonForDefeatRepository _reasonRepository = reasonRepository;

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