using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to list game results and details about outcomes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController(IResultService resultService) : ControllerBase
    {
        private readonly IResultService _resultService = resultService;

        /// <summary>
        /// Retrieves all recorded results.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var results = await _resultService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a single result by id.
        /// </summary>
        /// <param name="id">Result identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _resultService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"Result with ID {id} not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
