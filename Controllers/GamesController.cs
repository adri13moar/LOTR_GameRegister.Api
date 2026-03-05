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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            try
            {
                if (game == null) return BadRequest("Game data ar not valid.");
                int newId = await _gameRepository.CreateGameAsync(game);

                return Ok(new { Message = "Game registered successfully!", Id = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _gameRepository.DeleteByIdAsync(id);
            if (!deleted) return NotFound($"Game with ID ={id} not found.");

            return Ok(new { message = $"Game with ID = {id} delete correctly." });
        }

        [HttpPut("update/{id}")]
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