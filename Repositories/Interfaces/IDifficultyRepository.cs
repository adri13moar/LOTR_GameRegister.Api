using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public interface IDifficultyRepository
    {
        Task<IEnumerable<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetById(int id);
    }
}