using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Read-only data access for <see cref="Sphere"/> records backed by the <c>Spheres</c> table.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class SphereRepository(IConfiguration config)
        : ReadOnlyRepositoryBase<Sphere>(config, "Spheres", "Id"), ISphereRepository
    {
    }
}
