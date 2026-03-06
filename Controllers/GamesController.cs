using Microsoft.AspNetCore.Mvc;
using LOTR_GameRegister.Api.Repositories;
using LOTR_GameRegister.Api.Models;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController(GameRepository gameRepository) : ControllerBase
    {
        private readonly GameRepository _gameRepository = gameRepository;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var history = await _gameRepository.GetAllAsync();
                return Ok(history);
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
                var game = await _gameRepository.GetByIdAsync(id);
                if (game == null) return NotFound($"Game with ID {id} not found.");
                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            try
            {
                if (game == null) return BadRequest("Game data is not valid.");

                int newId = await _gameRepository.CreateGameAsync(game);
                game.Id = newId;

                return CreatedAtAction(nameof(GetAll), new { id = newId }, game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            bool deleted = await _gameRepository.DeleteByIdAsync(id);
            if (!deleted) return NotFound($"Game with ID ={id} not found.");

            return Ok(new { message = $"Game with ID = {id} delete correctly." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Game game)
        {
            try
            {
                game.Id = id;
                bool updated = await _gameRepository.UpdateAsync(game);

                if (!updated)
                {
                    return NotFound(new { message = $"Impossible to update. ID {id} not found." });
                }

                return Ok(new { message = $"Game with ID = {id} updated correctly ." });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}