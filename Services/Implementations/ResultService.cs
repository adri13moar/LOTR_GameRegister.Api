using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Services.Implementations
{
    public class ResultService(IResultRepository resultRepository) : IResultService
    {
        public async Task<IEnumerable<Result>> GetAllAsync()
            => await resultRepository.GetAllAsync();

        public async Task<Result?> GetByIdAsync(int id)
            => await resultRepository.GetByIdAsync(id);
    }
}
