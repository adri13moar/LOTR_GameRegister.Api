using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Services.Interfaces
{
    public interface ISphereService
    {
        Task<IEnumerable<Sphere>> GetAllAsync();
        Task<Sphere?> GetByIdAsync(int id);
    }
}
