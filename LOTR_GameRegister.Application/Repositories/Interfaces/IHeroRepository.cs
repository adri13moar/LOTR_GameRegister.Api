using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Repositories.Interfaces
{
    /// <summary>
    /// Provides data access for heroes.
    /// </summary>
    public interface IHeroRepository
    {
        /// <summary>
        /// Retrieves all heroes.
        /// </summary>
        /// <returns>All heroes in the register.</returns>
        Task<IEnumerable<Hero>> GetAllAsync();

        /// <summary>
        /// Retrieves a single hero by id.
        /// </summary>
        /// <param name="id">Hero identifier.</param>
        /// <returns>The matching hero, or <see langword="null"/> if not found.</returns>
        Task<Hero?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves the heroes matching the given ids.
        /// </summary>
        /// <param name="ids">Hero identifiers to look up.</param>
        /// <returns>The heroes whose ids were found.</returns>
        Task<IEnumerable<Hero>> GetByIdsAsync(List<int> ids);
    }
}
