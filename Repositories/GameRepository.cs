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
                SELECT 
                    G.Id as id, 
                    G.DatePlayed as datePlayed, 
                    G.IsCampaignMode as isCampaignMode,
                    G.Spheres as spheres,
                    G.DeadHeroes as deadHeroes,
                    G.Notes as notes,
                    Q.Name as questName, 
                    R.Name as result, 
                    D.Name as difficulty, 
                    H1.Name as hero1,
                    H2.Name as hero2,
                    H3.Name as hero3,
                    H4.Name as hero4,
                    RFD.Name as reason
                FROM Games G
                JOIN Quests Q ON G.QuestId = Q.Id
                JOIN Result R ON G.ResultId = R.Id
                JOIN Difficulties D ON G.DifficultyId = D.Id
                JOIN Heroes H1 ON G.Hero1Id = H1.Id
                LEFT JOIN Heroes H2 ON G.Hero2Id = H2.Id
                LEFT JOIN Heroes H3 ON G.Hero3Id = H3.Id
                LEFT JOIN Heroes H4 ON G.Hero4Id = H4.Id
                LEFT JOIN ReasonForDefeat RFD ON G.ReasonForDefeatId = RFD.Id
                ORDER BY G.DatePlayed DESC";

            return await db.QueryAsync(sql);
        }

        public async Task<bool> DeleteGameByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM Games WHERE Id = @Id";
            int rowsAffected = await db.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
