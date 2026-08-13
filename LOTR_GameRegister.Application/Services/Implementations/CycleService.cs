using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;

namespace LOTR_GameRegister.Application.Services.Implementations
{
    /// <summary>
    /// Provides business logic for cycles.
    /// </summary>
    /// <param name="cycleRepository">Data access for cycles.</param>
    public class CycleService(ICycleRepository cycleRepository) : ICycleService
    {
        private readonly ICycleRepository _cycleRepository = cycleRepository ?? throw new ArgumentNullException(nameof(cycleRepository));

        /// <inheritdoc />
        public async Task<IEnumerable<Cycle>> GetAllAsync()
            => await _cycleRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Cycle?> GetByIdAsync(int id)
            => await _cycleRepository.GetByIdAsync(id);
    }
}
