using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Api.Models;

namespace LOTR_GameRegister.Api.Repositories
{
    public class QuestRepository(IConfiguration config)
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;
        public async Task<IEnumerable<Quest>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT * 
                FROM Quests 
                ORDER BY Id";

            return await db.QueryAsync<Quest>(sql);
        }

        public async Task<IEnumerable<dynamic>> GetAllWithCycleAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
            SELECT 
                Q.*, 
                C.Name as cycle_name, 
                C.Category as category
            FROM Quests Q 
            JOIN Cycles C ON Q.CycleId = C.Id 
            ORDER BY C.Id, Q.Id";

            return await db.QueryAsync(sql);
        }

    }
}