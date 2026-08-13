using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Repositories.Interfaces
{
    /// <summary>
    /// Provides data access for spheres.
    /// </summary>
    public interface ISphereRepository
    {
        /// <summary>
        /// Retrieves all spheres.
        /// </summary>
        /// <returns>All spheres in the register.</returns>
        Task<IEnumerable<Sphere>> GetAllAsync();

        /// <summary>
        /// Retrieves a single sphere by id.
        /// </summary>
        /// <param name="id">Sphere identifier.</param>
        /// <returns>The matching sphere, or <see langword="null"/> if not found.</returns>
        Task<Sphere?> GetByIdAsync(int id);
    }
}
