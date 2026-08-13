using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var results = await _resultService.GetAllAsync();
            return Ok(results);
        }

        /// <summary>
        /// Retrieves a single result by id.
        /// </summary>
        /// <param name="id">Result identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _resultService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(Localizer.Get("ResultNotFound", id));
            }

            return Ok(result);
        }
    }
}
