using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;

namespace LOTR_GameRegister.Application.Services.Implementations
{
    /// <summary>
    /// Provides business logic for quests.
    /// </summary>
    /// <param name="questRepository">Data access for quests.</param>
    public class QuestService(IQuestRepository questRepository) : IQuestService
    {
        /// <inheritdoc />
        public async Task<IEnumerable<Quest>> GetAllAsync()
            => await questRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Quest?> GetByIdAsync(int id)
            => await questRepository.GetByIdAsync(id);
    }
}
