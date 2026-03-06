using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public class ReasonForDefeatRepository(IConfiguration config)
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<ReasonForDefeat>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT * 
                FROM ReasonForDefeat 
                ORDER BY Id";

            return await db.QueryAsync<ReasonForDefeat>(sql);
        }
    }
}
