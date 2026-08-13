using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Services.Interfaces
{
    /// <summary>
    /// Provides business logic for difficulty levels.
    /// </summary>
    public interface IDifficultyService
    {
        /// <summary>
        /// Retrieves all difficulty levels.
        /// </summary>
        /// <returns>All difficulty levels in the register.</returns>
        Task<IEnumerable<Difficulty>> GetAllAsync();

        /// <summary>
        /// Retrieves a single difficulty level by id.
        /// </summary>
        /// <param name="id">Difficulty identifier.</param>
        /// <returns>The matching difficulty, or <see langword="null"/> if not found.</returns>
        Task<Difficulty?> GetByIdAsync(int id);
    }
}
