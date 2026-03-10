using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Services.Interfaces
{
    public interface IReasonForDefeatService
    {
        Task<IEnumerable<ReasonForDefeat>> GetAllAsync();
        Task<ReasonForDefeat?> GetByIdAsync(int id);
    }
}
