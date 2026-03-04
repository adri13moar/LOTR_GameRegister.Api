using Dapper;
using Microsoft.Data.SqlClient;
using LOTR_GameRegister.Api.Models;

namespace LOTR_GameRegister.Api.Repositories
{
    public class GameRepository(IConfiguration config)
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        public async Task<int> CreateGameAsync(Game game)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
            INSERT INTO Games (QuestId, IsCampaignMode, DifficultyId, Hero1Id, Hero2Id, Hero3Id, Hero4Id, 
                               Spheres, DeadHeroes, ResultId, ReasonForDefeatId, DatePlayed, Notes)
            VALUES (@QuestId, @IsCampaignMode, @DifficultyId, @Hero1Id, @Hero2Id, @Hero3Id, @Hero4Id, 
                    @Spheres, @DeadHeroes, @ResultId, @ReasonForDefeatId, @DatePlayed, @Notes);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            return await db.QuerySingleAsync<int>(sql, game);
        }

        public async Task<IEnumerable<dynamic>> GetAllGamesAsync()
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
            SELECT G.Id, G.DatePlayed, Q.Name as QuestName, R.Name as Result, 
                   D.Name as Difficulty, H1.Name as Hero1, RFD.Name as Reason
            FROM Games G
            JOIN Quests Q ON G.QuestId = Q.Id
            JOIN Results R ON G.ResultId = R.Id
            JOIN Difficulties D ON G.DifficultyId = D.Id
            JOIN Heroes H1 ON G.Hero1Id = H1.Id
            LEFT JOIN ReasonForDefeat RFD ON G.ReasonForDefeatId = RFD.Id
            ORDER BY G.DatePlayed DESC";

            return await db.QueryAsync(sql);
        }
    }
}
