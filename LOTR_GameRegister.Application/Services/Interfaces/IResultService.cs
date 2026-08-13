using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Services.Interfaces
{
    /// <summary>
    /// Provides business logic for game results.
    /// </summary>
    public interface IResultService
    {
        /// <summary>
        /// Retrieves all game results.
        /// </summary>
        /// <returns>All results in the register.</returns>
        Task<IEnumerable<Result>> GetAllAsync();

        /// <summary>
        /// Retrieves a single result by id.
        /// </summary>
        /// <param name="id">Result identifier.</param>
        /// <returns>The matching result, or <see langword="null"/> if not found.</returns>
        Task<Result?> GetByIdAsync(int id);
    }
}
