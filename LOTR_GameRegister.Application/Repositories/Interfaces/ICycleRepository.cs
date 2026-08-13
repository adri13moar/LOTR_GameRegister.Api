using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Repositories.Interfaces
{
    /// <summary>
    /// Provides data access for cycles.
    /// </summary>
    public interface ICycleRepository
    {
        /// <summary>
        /// Retrieves all cycles.
        /// </summary>
        /// <returns>All cycles in the register.</returns>
        Task<IEnumerable<Cycle>> GetAllAsync();

        /// <summary>
        /// Retrieves a single cycle by id.
        /// </summary>
        /// <param name="id">Cycle identifier.</param>
        /// <returns>The matching cycle, or <see langword="null"/> if not found.</returns>
        Task<Cycle?> GetByIdAsync(int id);
    }
}