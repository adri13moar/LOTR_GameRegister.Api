using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Services.Interfaces
{
    public interface IResultService
    {
        Task<IEnumerable<Result>> GetAllAsync();
        Task<Result?> GetByIdAsync(int id);
    }
}
