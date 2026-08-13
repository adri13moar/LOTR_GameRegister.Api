using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Read-only data access for <see cref="Difficulty"/> records backed by the <c>Difficulties</c> table.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class DifficultyRepository(IConfiguration config)
        : ReadOnlyRepositoryBase<Difficulty>(config, "Difficulties", "Id"), IDifficultyRepository
    {
    }
}
