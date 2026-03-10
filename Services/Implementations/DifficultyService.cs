using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Services.Implementations
{
    public class DifficultyService(IDifficultyRepository difficultyRepository) : IDifficultyService
    {
        public async Task<IEnumerable<Difficulty>> GetAllAsync()
            => await difficultyRepository.GetAllAsync();

        public async Task<Difficulty?> GetByIdAsync(int id)
            => await difficultyRepository.GetById(id);
    }
}
