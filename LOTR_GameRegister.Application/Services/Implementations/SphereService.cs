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
        /// <inheritdoc />
        public async Task<IEnumerable<Sphere>> GetAllAsync()
            => await sphereRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<Sphere?> GetByIdAsync(int id)
            => await sphereRepository.GetByIdAsync(id);
    }
}
