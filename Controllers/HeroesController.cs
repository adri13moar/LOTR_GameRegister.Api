using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var heroes = await _heroService.GetAllAsync();
            return Ok(heroes);
        }

        /// <summary>
        /// Retrieves a hero by its id.
        /// </summary>
        /// <param name="id">Hero identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hero = await _heroService.GetByIdAsync(id);

            if (hero == null)
            {
                return NotFound(Localizer.Get("HeroNotFound", id));
            }

            return Ok(hero);
        }
    }
}
