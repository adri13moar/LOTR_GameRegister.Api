using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Manages game records (create, read, update, delete) in the register.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }


        /// <summary>
        /// Retrieves all game records.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        /// <summary>
        /// Retrieves a single game by id.
        /// </summary>
        /// <param name="id">Game identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound($"No se encontró la partida con ID {id}");

            return Ok(game);
        }


        /// <summary>
        /// Creates a new game record.
        /// </summary>
        /// <param name="game">Game payload.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            if (game == null) return BadRequest("Los datos de la partida son nulos.");

            var newId = await _gameService.CreateGameAsync(game);
            var createdGame = await _gameService.GetGameByIdAsync(newId);

            return CreatedAtAction(nameof(GetById), new { id = newId }, createdGame);
        }


        /// <summary>
        /// Updates an existing game.
        /// </summary>
        /// <param name="id">Identifier of the game to update.</param>
        /// <param name="game">Updated game object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Game game)
        {
            if (id != game.Id) return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");

            var success = await _gameService.UpdateGameAsync(game);
            if (!success) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a game by id.
        /// </summary>
        /// <param name="id">Identifier of the game to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _gameService.DeleteGameAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}