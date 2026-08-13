using Dapper;
using Microsoft.Data.SqlClient;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations;

/// <summary>
/// Shared implementation for simple read-only lookup repositories
/// that only need to fetch all rows or a single row by primary key.
/// </summary>
/// <typeparam name="T">The entity type mapped from the table.</typeparam>
/// <param name="config">Configuration used to obtain the connection string.</param>
/// <param name="tableName">Table name used by the SQL queries.</param>
/// <param name="orderBy">Column used for the default ordering.</param>
public abstract class ReadOnlyRepositoryBase<T>(IConfiguration config, string tableName, string orderBy = "Id") where T : class
{
    /// <summary>
    /// Connection string (DefaultConnection) used to open the database connection.
    /// </summary>
    private protected readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

    /// <summary>
    /// Returns every row of the table using the configured default ordering.
    /// </summary>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var db = new SqlConnection(_connectionString);
        return await db.QueryAsync<T>($"SELECT * FROM [{tableName}] ORDER BY {orderBy}");
    }

    /// <summary>
    /// Returns a single row by primary key, or <c>null</c> if not found.
    /// </summary>
    /// <param name="id">Primary key value.</param>
    public async Task<T?> GetByIdAsync(int id)
    {
        using var db = new SqlConnection(_connectionString);
        return await db.QueryFirstOrDefaultAsync<T>($"SELECT * FROM [{tableName}] WHERE Id = @Id", new { Id = id });
    }
}
