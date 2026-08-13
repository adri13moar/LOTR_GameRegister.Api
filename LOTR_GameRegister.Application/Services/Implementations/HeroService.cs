using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;
using System.ComponentModel.Design;

namespace LOTR_GameRegister.Application.Services.Implementations
{
    /// <summary>
    /// Provides business logic for heroes.
    /// </summary>
    /// <param name="heroRepository">Data access for heroes.</param>
    public class HeroService(IHeroRepository heroRepository) : IHeroService
    {
        private readonly IHeroRepository _heroRepository = heroRepository ?? throw new ArgumentNullException(nameof(heroRepository));

        /// <inheritdoc />
        public async Task<IEnumerable<Hero>> GetAllAsync()
            => await _heroRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Hero?> GetByIdAsync(int id)
            => await _heroRepository.GetByIdAsync(id);
    }
}