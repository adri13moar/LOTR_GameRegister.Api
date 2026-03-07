using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Repositories.Implementations;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// API endpoints for retrieving sphere data used in the game register.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SpheresController(ISphereRepository sphereRepository) : ControllerBase
    {
        private readonly ISphereRepository _sphereRepository = sphereRepository;

        /// <summary>
        /// Retrieves all spheres.
        /// </summary>
        /// <returns>List of spheres.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var spheres = await _sphereRepository.GetAllAsync();
                return Ok(spheres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a sphere by its identifier.
        /// </summary>
        /// <param name="id">Sphere identifier.</param>
        /// <returns>The requested sphere or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sphere = await _sphereRepository.GetByIdAsync(id);
                if (sphere == null)
                {
                    return NotFound($"Sphere with ID {id} not found.");
                }

                return Ok(sphere);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
