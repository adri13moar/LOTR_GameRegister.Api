using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Services.Interfaces
{
    public interface IDifficultyService
    {
        Task<IEnumerable<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetByIdAsync(int id);
    }
}
