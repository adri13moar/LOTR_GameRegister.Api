using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Api.Models;

namespace LOTR_GameRegister.Api.Repositories
{
    public class CycleRepository(IConfiguration config)
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Cycle>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT * 
                FROM Cycles 
                ORDER BY Id";

            return await db.QueryAsync<Cycle>(sql);
        }

        public async Task<Cycle?> GetByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT 
                    Id as id, 
                    Name as name, 
                    Category as category,
                    Name_es as name_es
                FROM Cycles 
                WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Cycle>(sql, new { Id = id });
        }
    }

}
