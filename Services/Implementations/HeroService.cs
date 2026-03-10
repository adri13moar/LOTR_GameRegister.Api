using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;
using System.ComponentModel.Design;

namespace LOTR_GameRegister.Api.Services.Implementations
{
    public class HeroService(IHeroRepository heroRepository) : IHeroService
    {
        public async Task<IEnumerable<Hero>> GetAllAsync()
            => await heroRepository.GetAllAsync();

        public async Task<Hero?> GetByIdAsync(int id)
            => await heroRepository.GetByIdAsync(id);
    }
}