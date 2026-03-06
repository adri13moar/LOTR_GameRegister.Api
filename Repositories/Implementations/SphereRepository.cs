using Dapper;
using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public class SphereRepository(IConfiguration config) : ISphereRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Sphere>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT 
                    Id as id,
                    Name as name,
                    Name_es as name_es
                FROM Spheres
                ORDER BY Id ASC";
            return await db.QueryAsync<Sphere>(sql);
        }

        public async Task<Sphere?> GetByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT 
                    Id as id, 
                    Name as name,
                    Name_es as name_es
                FROM Spheres 
                WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Sphere>(sql, new { Id = id });
        }
    }
}
