using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;

namespace LOTR_GameRegister.Application.Services.Implementations
{
    /// <summary>
    /// Provides business logic for spheres.
    /// </summary>
    /// <param name="sphereRepository">Data access for spheres.</param>
    public class SphereService(ISphereRepository sphereRepository) : ISphereService
    {
        private readonly ISphereRepository _sphereRepository = sphereRepository ?? throw new ArgumentNullException(nameof(sphereRepository));

        /// <inheritdoc />
        public async Task<IEnumerable<Sphere>> GetAllAsync()
            => await _sphereRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Sphere?> GetByIdAsync(int id)
            => await _sphereRepository.GetByIdAsync(id);
    }
}
