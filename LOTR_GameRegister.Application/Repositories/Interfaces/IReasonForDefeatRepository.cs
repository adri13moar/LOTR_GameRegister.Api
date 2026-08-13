using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Repositories.Interfaces
{
    /// <summary>
    /// Provides data access for reasons for defeat.
    /// </summary>
    public interface IReasonForDefeatRepository
    {
        /// <summary>
        /// Retrieves all reasons for defeat.
        /// </summary>
        /// <returns>All reasons for defeat in the register.</returns>
        Task<IEnumerable<ReasonForDefeat>> GetAllAsync();

        /// <summary>
        /// Retrieves a single reason for defeat by id.
        /// </summary>
        /// <param name="id">Reason for defeat identifier.</param>
        /// <returns>The matching reason for defeat, or <see langword="null"/> if not found.</returns>
        Task<ReasonForDefeat?> GetByIdAsync(int id);
    }
}