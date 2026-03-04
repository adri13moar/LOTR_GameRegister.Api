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
                    Id as id, 
                    DatePlayed as datePlayed, 
                    IsCampaignMode as isCampaignMode,
                    QuestId as questId,
                    DifficultyId as difficultyId,
                    Hero1Id as hero1Id,
                    Hero2Id as hero2Id,
                    Hero3Id as hero3Id,
                    Hero4Id as hero4Id,
                    Spheres as spheres,
                    DeadHeroes as deadHeroes,
                    ResultId as resultId,
                    ReasonForDefeatId as reasonId,
                    Notes as notes
                FROM Games
                ORDER BY DatePlayed DESC";

            return await db.QueryAsync(sql);
        }

        public async Task<bool> DeleteGameByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM Games WHERE Id = @Id";
            int rowsAffected = await db.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateGameAsync(Game game)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                UPDATE Games 
                SET QuestId = @QuestId, 
                    IsCampaignMode = @IsCampaignMode, 
                    DifficultyId = @DifficultyId, 
                    Hero1Id = @Hero1Id, 
                    Hero2Id = @Hero2Id, 
                    Hero3Id = @Hero3Id, 
                    Hero4Id = @Hero4Id, 
                    Spheres = @Spheres, 
                    DeadHeroes = @DeadHeroes, 
                    ResultId = @ResultId, 
                    ReasonForDefeatId = @ReasonForDefeatId, 
                    DatePlayed = @DatePlayed, 
                    Notes = @Notes
                WHERE Id = @Id";

            int rowsAffected = await db.ExecuteAsync(sql, game);
            return rowsAffected > 0;
        }
    }
}
