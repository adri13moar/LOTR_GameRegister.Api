using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Read-only data access for <see cref="ReasonForDefeat"/> records backed by the <c>ReasonsForDefeat</c> table.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class ReasonForDefeatRepository(IConfiguration config)
        : ReadOnlyRepositoryBase<ReasonForDefeat>(config, "ReasonsForDefeat", "Id"), IReasonForDefeatRepository
    {
    }
}
