using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public class DifficultyRepository(IConfiguration config)
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;
        public async Task<IEnumerable<Difficulty>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT * 
                FROM Difficulties 
                ORDER BY Id";

            return await db.QueryAsync<Difficulty>(sql);
        }

        public async Task<Difficulty?> GetById(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT
                    Id as id,
                    Name as name,
                    Name_es as name_es
                FROM Difficulties
                WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Difficulty>(sql, new { Id = id });
        }
    }
}
