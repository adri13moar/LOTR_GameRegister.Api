using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Services.Implementations
{
    public class ReasonForDefeatService(IReasonForDefeatRepository reasonForDefeatRepository) : IReasonForDefeatService
    {
        public async Task<IEnumerable<ReasonForDefeat>> GetAllAsync()
            => await reasonForDefeatRepository.GetAllAsync();

        public async Task<ReasonForDefeat?> GetByIdAsync(int id)
            => await reasonForDefeatRepository.GetByIdAsync(id);
    }
}
