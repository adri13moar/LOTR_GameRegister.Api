using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Data access for <see cref="Quest"/> records, populating the related <see cref="Cycle"/> navigation property.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class QuestRepository(IConfiguration config) : IQuestRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Retrieves all quests with their cycle populated (INNER JOIN on Cycles).
        /// </summary>
        /// <returns>All quests, each with its <see cref="Quest.Cycle"/> set.</returns>
        public async Task<IEnumerable<Quest>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT 
                    q.Id as id, 
                    q.Name as name, 
                    q.Name_es as name_es, 
                    q.CommunityDifficulty as community_difficulty, 
                    c.Id as id,
                    c.Name as name, 
                    c.Name_es as name_es, 
                    c.Category as category
                FROM Quests q
                INNER JOIN Cycles c ON q.CycleId = c.Id
                ORDER BY q.Id";

            return await db.QueryAsync<Quest, Cycle, Quest>(
                sql,
                (quest, cycle) =>
                {
                    quest.Cycle = cycle;
                    quest.CycleId = cycle.Id;
                    return quest;
                },
                splitOn: "Id"
            );
        }

        /// <summary>
        /// Retrieves a single quest by id with its cycle populated, or <c>null</c> if not found.
        /// </summary>
        /// <param name="id">Identifier of the quest to retrieve.</param>
        /// <returns>The matching quest with its cycle, or <c>null</c>.</returns>
        public async Task<Quest?> GetByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT 
                    q.Id, 
                    q.Name, 
                    q.Name_es, 
                    q.CommunityDifficulty,
                    c.Id, 
                    c.Name, 
                    c.Name_es, 
                    c.Category
                FROM Quests q
                INNER JOIN Cycles c ON q.CycleId = c.Id
                WHERE q.Id = @Id";

            var quests = await db.QueryAsync<Quest, Cycle, Quest>(
                sql,
                (quest, cycle) =>
                {
                    quest.Cycle = cycle;
                    quest.CycleId = cycle.Id;
                    return quest;
                },
                new { Id = id },
                splitOn: "Id"
            );

            return quests.FirstOrDefault();
        }

    }
}