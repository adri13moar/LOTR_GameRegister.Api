using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;

namespace LOTR_GameRegister.Application.Services.Implementations
{
    /// <summary>
    /// Provides business logic for game results.
    /// </summary>
    /// <param name="resultRepository">Data access for game results.</param>
    public class ResultService(IResultRepository resultRepository) : IResultService
    {
        /// <inheritdoc />
        public async Task<IEnumerable<Result>> GetAllAsync()
            => await resultRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Result?> GetByIdAsync(int id)
            => await resultRepository.GetByIdAsync(id);
    }
}
