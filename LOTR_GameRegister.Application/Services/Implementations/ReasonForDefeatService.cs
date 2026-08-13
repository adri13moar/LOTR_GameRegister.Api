using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;

namespace LOTR_GameRegister.Application.Services.Implementations
{
    /// <summary>
    /// Provides business logic for reasons for defeat.
    /// </summary>
    /// <param name="reasonForDefeatRepository">Data access for reasons for defeat.</param>
    public class ReasonForDefeatService(IReasonForDefeatRepository reasonForDefeatRepository) : IReasonForDefeatService
    {
        /// <inheritdoc />
        public async Task<IEnumerable<ReasonForDefeat>> GetAllAsync()
            => await reasonForDefeatRepository.GetAllAsync();

        /// <inheritdoc />
        public async Task<ReasonForDefeat?> GetByIdAsync(int id)
            => await reasonForDefeatRepository.GetByIdAsync(id);
    }
}
