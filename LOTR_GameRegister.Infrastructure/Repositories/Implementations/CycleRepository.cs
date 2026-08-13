using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Read-only data access for <see cref="Cycle"/> records backed by the <c>Cycles</c> table.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class CycleRepository(IConfiguration config)
        : ReadOnlyRepositoryBase<Cycle>(config, "Cycles", "Id"), ICycleRepository
    {
    }
}
