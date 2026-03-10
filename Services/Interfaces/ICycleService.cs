using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Services.Interfaces
{
    public interface ICycleService
    {
        Task<IEnumerable<Cycle>> GetAllAsync();
        Task<Cycle?> GetByIdAsync(int id);
    }
}
