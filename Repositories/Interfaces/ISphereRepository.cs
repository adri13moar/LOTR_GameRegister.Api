using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Interfaces
{
    public interface ISphereRepository
    {
        Task<IEnumerable<Sphere>> GetAllAsync();
        Task<Sphere?> GetByIdAsync(int id);
    }
}
