using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Services.Implementations
{
    public class QuestService(IQuestRepository questRepository) : IQuestService
    {
        public async Task<IEnumerable<Quest>> GetAllAsync()
            => await questRepository.GetAllAsync();

        public async Task<Quest?> GetByIdAsync(int id)
            => await questRepository.GetByIdAsync(id);
    }
}
