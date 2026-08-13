using Dapper;
using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Read-only data access for <see cref="Hero"/> records backed by the <c>Heroes</c> table, ordered by name.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class HeroRepository(IConfiguration config)
        : ReadOnlyRepositoryBase<Hero>(config, "Heroes", "Name"), IHeroRepository
    {
        /// <summary>
        /// Retrieves all heroes whose identifier is contained in the given list.
        /// </summary>
        /// <param name="ids">Hero identifiers to fetch.</param>
        /// <returns>The matching heroes.</returns>
        public async Task<IEnumerable<Hero>> GetByIdsAsync(List<int> ids)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT * 
                FROM Heroes
                WHERE Id IN @ids";

            return await db.QueryAsync<Hero>(sql, new { ids });
        }
    }
}
