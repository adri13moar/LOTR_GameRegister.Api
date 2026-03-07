using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public interface IReasonForDefeatRepository
    {
        Task<IEnumerable<ReasonForDefeat>> GetAllAsync();
        Task<ReasonForDefeat?> GetByIdAsync(int id);
    }
}