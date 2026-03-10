using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Services.Implementations
{
    public class CycleService(ICycleRepository cycleRepository) : ICycleService
    {
        public async Task<IEnumerable<Cycle>> GetAllAsync()
            => await cycleRepository.GetAllAsync();

        public async Task<Cycle?> GetByIdAsync(int id)
            => await cycleRepository.GetByIdAsync(id);
    }
}
