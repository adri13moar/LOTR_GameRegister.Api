using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public interface IResultRepository
    {
        Task<IEnumerable<Result>> GetAllAsync();
        Task<Result?> GetByIdAsync(int id);
    }
}