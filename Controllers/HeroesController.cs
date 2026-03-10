using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Models;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to access hero information used in the register.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController(IHeroService heroService) : ControllerBase
    {
        private readonly IHeroService _heroService = heroService;

        /// <summary>
        /// Retrieves all heroes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var heroes = await _heroService.GetAllAsync();
                return Ok(heroes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a hero by its id.
        /// </summary>
        /// <param name="id">Hero identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var hero = await _heroService.GetByIdAsync(id);

                if (hero == null)
                {
                    return NotFound($"Hero with ID {id} not found.");
                }

                return Ok(hero);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}