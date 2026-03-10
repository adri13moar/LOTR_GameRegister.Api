using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Services.Interfaces
{
    public interface IQuestService
    {
        Task<IEnumerable<Quest>> GetAllAsync();
        Task<Quest?> GetByIdAsync(int id);
    }
}
