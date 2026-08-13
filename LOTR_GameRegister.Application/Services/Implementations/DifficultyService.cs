using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;

namespace LOTR_GameRegister.Application.Services.Implementations
{
    /// <summary>
    /// Provides business logic for difficulty levels.
    /// </summary>
    /// <param name="difficultyRepository">Data access for difficulty levels.</param>
    public class DifficultyService(IDifficultyRepository difficultyRepository) : IDifficultyService
    {
        /// <inheritdoc />
        public async Task<IEnumerable<Difficulty>> GetAllAsync()
            => await difficultyRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Difficulty?> GetByIdAsync(int id)
            => await difficultyRepository.GetByIdAsync(id);
    }
}
