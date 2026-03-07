using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public interface ICycleRepository
    {
        Task<IEnumerable<Cycle>> GetAllAsync();
        Task<Cycle?> GetByIdAsync(int id);
    }
}