using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Interfaces
{
    public interface IHeroRepository
    {
        Task<IEnumerable<Hero>> GetAllAsync();
        Task<Hero?> GetByIdAsync(int id);
        Task<IEnumerable<Hero>> GetByIdsAsync(List<int> ids);
    }
}
