using Dapper;
using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public class HeroRepository(IConfiguration config) : IHeroRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Hero>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT 
                    Id, 
                    Name, 
                    Name_es, 
                    SphereId, 
                    StartingThreat, 
                    RingsDbId
                FROM Heroes
                ORDER BY Name ASC";

            return await db.QueryAsync<Hero>(sql);
        }

        public async Task<Hero?> GetByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT 
                    Id, 
                    Name, 
                    Name_es, 
                    SphereId, 
                    StartingThreat, 
                    RingsDbId
                FROM Heroes 
                WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Hero>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Hero>> GetByIdsAsync(List<int> ids)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT * 
                FROM Heroes
                WHERE Id IN @ids";

            return await db.QueryAsync<Hero>( sql, new { ids });
        }
    }
}