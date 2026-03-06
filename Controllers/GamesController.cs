using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LOTR_GameRegister.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound($"No se encontró la partida con ID {id}");

            return Ok(game);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            if (game == null) return BadRequest("Los datos de la partida son nulos.");

            var newId = await _gameService.CreateGameAsync(game);
            var createdGame = await _gameService.GetGameByIdAsync(newId);

            return CreatedAtAction(nameof(GetById), new { id = newId }, createdGame);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Game game)
        {
            if (id != game.Id) return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");

            var success = await _gameService.UpdateGameAsync(game);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _gameService.DeleteGameAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}