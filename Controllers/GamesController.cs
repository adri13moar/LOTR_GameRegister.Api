using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LOTR_GameRegister.Api.Controllers
{
    /// <summary>
    /// Manages game records (create, read, update, delete) in the register.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GamesController(IGameService gameService) : ControllerBase
    {
        /// <summary>
        /// Retrieves all game records.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await gameService.GetAllGamesAsync();
            return Ok(games);
        }

        /// <summary>
        /// Retrieves a single game by id.
        /// </summary>
        /// <param name="id">Game identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound(Localizer.Get("GameNotFound", id));

            return Ok(game);
        }

        /// <summary>
        /// Creates a new game record.
        /// </summary>
        /// <param name="dto">Game payload.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameDto dto)
        {
            var newId = await gameService.CreateGameAsync(dto);
            var createdGame = await gameService.GetGameByIdAsync(newId);

            return CreatedAtAction(nameof(GetById), new { id = newId }, createdGame);
        }

        /// <summary>
        /// Updates an existing game.
        /// </summary>
        /// <param name="id">Identifier of the game to update.</param>
        /// <param name="dto">Updated game object.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GameDto dto)
        {
            if (id != dto.Id) return BadRequest(Localizer.Get("IdMismatch"));

            var success = await gameService.UpdateGameAsync(dto);
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
            var success = await gameService.DeleteGameAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
