using Dapper;
using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Data access for <see cref="User"/> records backed by the <c>Users</c> table.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class UserRepository(IConfiguration config) : IUserRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Inserts a new user and returns the new row id. Does not hash the password itself — the caller supplies the hash in <see cref="User.PasswordHash"/>.
        /// </summary>
        /// <param name="user">The user to create, with <see cref="User.PasswordHash"/> already set.</param>
        /// <returns>The auto-generated identifier of the newly created user.</returns>
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

        /// <summary>
        /// Retrieves all users ordered by id.
        /// </summary>
        /// <returns>All users.</returns>
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

        /// <summary>
        /// Retrieves a user by exact username, or <c>null</c> if not found.
        /// </summary>
        /// <param name="username">Username to look up.</param>
        /// <returns>The matching user, or <c>null</c>.</returns>
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

        /// <summary>
        /// Retrieves a user by exact email address, or <c>null</c> if not found.
        /// </summary>
        /// <param name="email">Email address to look up.</param>
        /// <returns>The matching user, or <c>null</c>.</returns>
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

        /// <summary>
        /// Returns whether any user already matches the given username or email.
        /// </summary>
        /// <param name="username">Username to check for.</param>
        /// <param name="email">Email address to check for.</param>
        /// <returns><c>true</c> if the username or email is already in use; otherwise <c>false</c>.</returns>
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