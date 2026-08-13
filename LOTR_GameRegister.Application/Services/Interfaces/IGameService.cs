using LOTR_GameRegister.Application.Models.Dto;

namespace LOTR_GameRegister.Application.Services.Interfaces
{
    /// <summary>
    /// Provides business logic for game records.
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Retrieves all game records.
        /// </summary>
        /// <returns>All games in the register.</returns>
        Task<IEnumerable<GameDto>> GetAllGamesAsync();

        /// <summary>
        /// Retrieves a single game by id.
        /// </summary>
        /// <param name="id">Game identifier.</param>
        /// <returns>The matching game, or <see langword="null"/> if not found.</returns>
        Task<GameDto?> GetGameByIdAsync(int id);

        /// <summary>
        /// Creates a new game record, recomputing derived values (dead heroes and spheres).
        /// </summary>
        /// <param name="game">The game payload to create.</param>
        /// <returns>The id of the newly created game.</returns>
        Task<int> CreateGameAsync(CreateGameDto game);

        /// <summary>
        /// Updates an existing game record, recomputing derived values (dead heroes and spheres).
        /// </summary>
        /// <param name="game">The game with updated values.</param>
        /// <returns><see langword="true"/> if the game was updated; otherwise, <see langword="false"/>.</returns>
        Task<bool> UpdateGameAsync(GameDto game);

        /// <summary>
        /// Deletes a game by id.
        /// </summary>
        /// <param name="id">Identifier of the game to delete.</param>
        /// <returns><see langword="true"/> if the game was deleted; otherwise, <see langword="false"/>.</returns>
        Task<bool> DeleteGameAsync(int id);
    }
}
