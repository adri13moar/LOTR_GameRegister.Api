using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Repositories.Interfaces
{
    /// <summary>
    /// Provides data access for games.
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Creates a new game record.
        /// </summary>
        /// <param name="game">The game to create.</param>
        /// <returns>The id of the newly created game.</returns>
        Task<int> CreateAsync(Game game);

        /// <summary>
        /// Retrieves all game records.
        /// </summary>
        /// <returns>All games in the register.</returns>
        Task<IEnumerable<Game>> GetAllAsync();

        /// <summary>
        /// Retrieves a single game by id.
        /// </summary>
        /// <param name="id">Game identifier.</param>
        /// <returns>The matching game, or <see langword="null"/> if not found.</returns>
        Task<Game?> GetByIdAsync(int id);

        /// <summary>
        /// Updates an existing game record.
        /// </summary>
        /// <param name="game">The game with updated values.</param>
        /// <returns><see langword="true"/> if the game was updated; otherwise, <see langword="false"/>.</returns>
        Task<bool> UpdateAsync(Game game);

        /// <summary>
        /// Deletes a game by id.
        /// </summary>
        /// <param name="id">Identifier of the game to delete.</param>
        /// <returns><see langword="true"/> if the game was deleted; otherwise, <see langword="false"/>.</returns>
        Task<bool> DeleteByIdAsync(int id);
    }
}
