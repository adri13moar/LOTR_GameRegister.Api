using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Services.Interfaces
{
    /// <summary>
    /// Provides business logic for quests.
    /// </summary>
    public interface IQuestService
    {
        /// <summary>
        /// Retrieves all quests.
        /// </summary>
        /// <returns>All quests in the register.</returns>
        Task<IEnumerable<Quest>> GetAllAsync();

        /// <summary>
        /// Retrieves a single quest by id.
        /// </summary>
        /// <param name="id">Quest identifier.</param>
        /// <returns>The matching quest, or <see langword="null"/> if not found.</returns>
        Task<Quest?> GetByIdAsync(int id);
    }
}
