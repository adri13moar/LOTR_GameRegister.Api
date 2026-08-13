using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Services.Interfaces
{
    /// <summary>
    /// Provides business logic for heroes.
    /// </summary>
    public interface IHeroService
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
    }
}
