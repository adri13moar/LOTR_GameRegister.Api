using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Api.Models;

namespace LOTR_GameRegister.Api.Repositories
{
    public class HeroRepository(IConfiguration config)
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Hero>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT 
                    Id as id, 
                    Name as name, 
                    Name_es as name_es,
                    SphereId as sphere_id 
                    StartingThreat as starting_thread, 
                    RingsDbId as rings_db_id
                FROM Heroes 
                ORDER BY Name ASC";

            return await db.QueryAsync<Hero>(sql);
        }

        public async Task<Hero?> GetByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT 
                    Id as id, 
                    Name as name, 
                    Name_es as name_es,
                    StartingThreat as starting_thread, 
                    SphereId as sphere_id,
                    RingsDbId as rings_db_id
                FROM Heroes 
                WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Hero>(sql, new { Id = id });
        }
    }
}