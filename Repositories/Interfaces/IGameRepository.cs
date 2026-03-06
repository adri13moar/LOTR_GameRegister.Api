using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<int> CreateAsync(Game game);
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Game game);
        Task<bool> DeleteByIdAsync(int id);
    }
}
