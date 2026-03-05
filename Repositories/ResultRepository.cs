using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Api.Models;

namespace LOTR_GameRegister.Api.Repositories
{
    public class ResultRepository(IConfiguration config)
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Result>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT * 
                FROM Results 
                ORDER BY Id";

            return await db.QueryAsync<Result>(sql);
        }

        public async Task<Result?> GetByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT
                    Id as id,
                    Name as name,
                    Name_es as name
                FROM Results
                WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Result>(sql, new { Id = id });
        }
    }
}
