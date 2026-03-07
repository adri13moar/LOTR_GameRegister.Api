using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Repositories.Implementations;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpheresController(ISphereRepository sphereRepository) : ControllerBase
    {
        private readonly ISphereRepository _sphereRepository = sphereRepository;

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
