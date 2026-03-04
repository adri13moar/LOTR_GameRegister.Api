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
            const string sql = "SELECT * FROM Heroes ORDER BY Name";

            return await db.QueryAsync<Hero>(sql);
        }
    }
}