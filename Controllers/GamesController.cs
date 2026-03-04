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
        public async Task<IActionResult> GetHistory()
        {
            try
            {
                var history = await _gameRepository.GetAllGamesAsync();
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error at getting Games History: {ex.Message}");
            }
        }

        [HttpPost]
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
                return StatusCode(500, $"Error at registring game: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _gameRepository.DeleteGameByIdAsync(id);
            if (!deleted) return NotFound($"Game with ID ={id} not found.");

            return Ok(new { message = $"Game with ID = {id} correctly deleted." });
        }
    }
}