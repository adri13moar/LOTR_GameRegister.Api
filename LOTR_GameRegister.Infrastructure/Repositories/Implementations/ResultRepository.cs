using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Read-only data access for <see cref="Result"/> records backed by the <c>Results</c> table.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class ResultRepository(IConfiguration config)
        : ReadOnlyRepositoryBase<Result>(config, "Results", "Id"), IResultRepository
    {
    }
}
