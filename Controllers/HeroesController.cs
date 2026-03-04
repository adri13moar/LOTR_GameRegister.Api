using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Repositories;
using LOTR_GameRegister.Api.Models;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController(HeroRepository heroRepository) : ControllerBase
    {
        private readonly HeroRepository _heroRepository = heroRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var heroes = await _heroRepository.GetAllAsync();
                return Ok(heroes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}