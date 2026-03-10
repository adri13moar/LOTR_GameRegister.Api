using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Services.Implementations
{
    public class SphereService(ISphereRepository sphereRepository) : ISphereService
    {
        public async Task<IEnumerable<Sphere>> GetAllAsync()
            => await sphereRepository.GetAllAsync();

        public async Task<Sphere?> GetByIdAsync(int id)
            => await sphereRepository.GetByIdAsync(id);
    }
}
