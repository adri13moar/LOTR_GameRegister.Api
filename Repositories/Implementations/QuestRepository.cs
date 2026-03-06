using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Api.Models.Entities;

namespace LOTR_GameRegister.Api.Repositories.Implementations
{
    public class QuestRepository(IConfiguration config)
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<IEnumerable<Quest>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT 
                    q.Id as id, 
                    q.Name as name, 
                    q.Name_es as name_es, 
                    q.CommunityDifficulty as community_difficulty, 
                    q.CycleId as cycle_id,
                    c.Id as id,
                    c.Name as name, 
                    c.Name_es as name_es, 
                    c.Category as category
                FROM Quests q
                INNER JOIN Cycles c ON q.CycleId = c.Id";

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