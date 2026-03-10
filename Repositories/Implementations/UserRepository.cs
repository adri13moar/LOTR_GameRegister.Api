using Dapper;
using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public class UserRepository(IConfiguration config) : IUserRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<int> CreateAsync(User user)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                INSERT INTO Users (
                    Username, 
                    Email, 
                    PasswordHash, 
                    Role, 
                    CreatedAt
                )
                VALUES (
                    @Username, 
                    @Email, 
                    @PasswordHash, 
                    @Role, 
                    @CreatedAt
                );
                SELECT CAST(SCOPE_IDENTITY() as int);";

            return await db.ExecuteScalarAsync<int>(sql, user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT 
                    Id,
                    Username, 
                    Email, 
                    PasswordHash, 
                    Role, 
                    CreatedAt
                FROM Users
                ORDER BY Id ASC";

            return await db.QueryAsync<User>(sql);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT 
                    Id, 
                    Username, 
                    Email, 
                    PasswordHash, 
                    Role, 
                    CreatedAt 
                FROM Users 
                WHERE Username = @Username";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT 
                    Id, 
                    Username, 
                    Email, 
                    PasswordHash, 
                    Role, 
                    CreatedAt 
                FROM Users 
                WHERE Email = @Email";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task<bool> ExistsAsync(string username, string email)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT COUNT(1) 
                FROM Users 
                WHERE Username = @Username OR Email = @Email";

            var count = await db.ExecuteScalarAsync<int>(sql, new { Username = username, Email = email });
            return count > 0;
        }
    }
}