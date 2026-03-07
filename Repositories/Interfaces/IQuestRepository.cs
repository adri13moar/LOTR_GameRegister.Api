using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Interfaces
{
    public interface IQuestRepository
    {
        Task<IEnumerable<Quest>> GetAllAsync();
        Task<Quest?> GetByIdAsync(int id);
    }
}