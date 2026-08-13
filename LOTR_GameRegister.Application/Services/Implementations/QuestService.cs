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
        private readonly IQuestRepository _questRepository = questRepository ?? throw new ArgumentNullException(nameof(questRepository));

        /// <inheritdoc />
        public async Task<IEnumerable<Quest>> GetAllAsync()
            => await _questRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Quest?> GetByIdAsync(int id)
            => await _questRepository.GetByIdAsync(id);
    }
}
