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
            await db.OpenAsync();
            using var transaction = db.BeginTransaction();

            try
            {
                const string sqlGame = @"
                    INSERT INTO Games (
                        QuestId, 
                        IsCampaignMode, 
                        DifficultyId, 
                        Spheres, 
                        DeadHeroes, 
                        ResultId, 
                        ReasonForDefeatId, 
                        DatePlayed, 
                        Notes)
                VALUES (
                        @QuestId, 
                        @IsCampaignMode, 
                        @DifficultyId, 
                        @Spheres, 
                        @DeadHeroes, 
                        @ResultId, 
                        @ReasonForDefeatId, 
                        @DatePlayed, 
                        @Notes);
                SELECT CAST(SCOPE_IDENTITY() as int);";

                int gameId = await db.QuerySingleAsync<int>(sqlGame, game, transaction);

                if (game.Heroes != null && game.Heroes.Any())
                {
                    const string sqlHeroes = @"
                        INSERT INTO GameHeroes (
                            GameId, 
                            HeroId) 
                        VALUES (
                            @GameId, 
                            @HeroId)";

                    var batchHeroes = game.Heroes.Select(h => new { GameId = gameId, HeroId = h.Id });

                    await db.ExecuteAsync(sqlHeroes, batchHeroes, transaction);
                }

                transaction.Commit();
                return gameId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT g.*, h.*
                FROM Games g
                LEFT JOIN GameHeroes gh ON g.Id = gh.GameId
                LEFT JOIN Heroes h ON gh.HeroId = h.Id
                ORDER BY g.DatePlayed DESC";

            var gameDictionary = new Dictionary<int, Game>();

            var result = await db.QueryAsync<Game, Hero, Game>(
                sql,
                (game, hero) =>
                {
                    if (!gameDictionary.TryGetValue(game.Id, out var gameEntry))
                    {
                        gameEntry = game;
                        gameEntry.Heroes = new List<Hero>();
                        gameDictionary.Add(gameEntry.Id, gameEntry);
                    }

                    if (hero != null)
                    {
                        gameEntry.Heroes.Add(hero);
                    }

                    return gameEntry;
                },
                splitOn: "Id"
            );

            return gameDictionary.Values;
        }

        public async Task<bool> UpdateAsync(Game game)
        {
            using var db = new SqlConnection(_connectionString);

            await db.OpenAsync();

            using var transaction = db.BeginTransaction();

            try
            {
                const string sqlUpdateGame = @"
                UPDATE Games 
                SET QuestId = @QuestId, 
                    IsCampaignMode = @IsCampaignMode, 
                    DifficultyId = @DifficultyId, 
                    Spheres = @Spheres, 
                    DeadHeroes = @DeadHeroes, 
                    ResultId = @ResultId, 
                    ReasonForDefeatId = @ReasonForDefeatId, 
                    DatePlayed = @DatePlayed, 
                    Notes = @Notes
                WHERE Id = @Id";

                await db.ExecuteAsync(sqlUpdateGame, game, transaction);

                const string sqlDeleteHeroes = @"
                    DELETE FROM GameHeroes 
                    WHERE GameId = @Id";

                await db.ExecuteAsync(sqlDeleteHeroes, new { Id = game.Id }, transaction);

                if (game.Heroes != null && game.Heroes.Any())
                {
                    const string sqlInsertHeroes = @"
                        INSERT INTO GameHeroes (
                            GameId, 
                            HeroId) 
                        VALUES (
                            @GameId, 
                            @HeroId)";

                    var batchHeroes = game.Heroes.Select(h => new { GameId = game.Id, HeroId = h.Id });

                    await db.ExecuteAsync(sqlInsertHeroes, batchHeroes, transaction);
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                DELETE FROM Games 
                WHERE Id = @Id";

            int rowsAffected = await db.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0;
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                SELECT g.*, h.*
                FROM Games g
                LEFT JOIN GameHeroes gh ON g.Id = gh.GameId
                LEFT JOIN Heroes h ON gh.HeroId = h.Id
                WHERE g.Id = @Id";

            var gameDictionary = new Dictionary<int, Game>();

            var result = await db.QueryAsync<Game, Hero, Game>(
                sql,
                (game, hero) =>
                {
                    if (!gameDictionary.TryGetValue(game.Id, out var currentGame))
                    {
                        currentGame = game;
                        currentGame.Heroes = new List<Hero>();
                        gameDictionary.Add(currentGame.Id, currentGame);
                    }

                    if (hero != null)
                    {
                        currentGame.Heroes.Add(hero);
                    }
                    return currentGame;
                },
                new { Id = id },
                splitOn: "Id"
            );

            return result.Distinct().FirstOrDefault();
        }
    }
}
