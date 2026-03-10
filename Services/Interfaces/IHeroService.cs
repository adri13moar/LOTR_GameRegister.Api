using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Services.Interfaces
{
    public interface IHeroService
    {
        Task<IEnumerable<Hero>> GetAllAsync();
        Task<Hero?> GetByIdAsync(int id);
    }
}
