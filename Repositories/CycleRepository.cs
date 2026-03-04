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
            const string sql = "SELECT * FROM Cycles ORDER BY Id";

            return await db.QueryAsync<Cycle>(sql);
        }
    }
}
