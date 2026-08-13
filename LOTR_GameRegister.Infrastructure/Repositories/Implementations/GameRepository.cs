using Dapper;
using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace LOTR_GameRegister.Infrastructure.Repositories.Implementations
{
    /// <summary>
    /// Data access for <see cref="Game"/> records, including the heroes linked through the GameHeroes join table.
    /// </summary>
    /// <param name="config">Configuration used to obtain the connection string.</param>
    public class GameRepository(IConfiguration config) : IGameRepository
    {
        private readonly string _connectionString = config.GetConnectionString("DefaultConnection")!;

        /// <summary>
        /// Inserts the game and its heroes (as GameHeroes rows carrying the IsDead flag) in a single transaction.
        /// </summary>
        /// <param name="game">The game to create, including its heroes.</param>
        /// <returns>The auto-generated identifier of the newly created game.</returns>
        public async Task<int> CreateAsync(Game game)
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
                            HeroId,
                            IsDead) 
                        VALUES (
                            @GameId, 
                            @HeroId,
                            @IsDead)";

                    var batchHeroes = game.Heroes.Select(h => new 
                    { 
                        GameId = gameId,
                        HeroId = h.Id,
                        IsDead = h.IsDead
                    });

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

        /// <summary>
        /// Retrieves all games ordered by date played (newest first), mapping each game's heroes via Dapper multi-mapping.
        /// </summary>
        /// <returns>All games, each with its <see cref="Game.Heroes"/> populated from the GameHeroes join table.</returns>
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

        /// <summary>
        /// Retrieves a single game by id with its heroes (including the IsDead flag read from the GameHeroes join table), or <c>null</c> if not found.
        /// </summary>
        /// <param name="id">Identifier of the game to retrieve.</param>
        /// <returns>The matching game with its heroes, or <c>null</c>.</returns>
        public async Task<Game?> GetByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);
            const string sql = @"
                SELECT g.*, h.*, gh.IsDead 
                FROM Games g
                LEFT JOIN GameHeroes gh ON g.Id = gh.GameId
                LEFT JOIN Heroes h ON gh.HeroId = h.Id
                WHERE g.Id = @id";

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
                new { id },
                splitOn: "Id"
            );

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Updates the game row and rewrites its GameHeroes links (delete + re-insert) in a single transaction.
        /// </summary>
        /// <param name="game">The game to update, including the current hero list.</param>
        /// <returns><c>true</c> if the update succeeded; otherwise <c>false</c> when the game does not exist.</returns>
        /// <exception cref="Exception">Re-thrown from the database when the update fails.</exception>
        public async Task<bool> UpdateAsync(Game game)
        {
            using var db = new SqlConnection(_connectionString);

            await db.OpenAsync();

            using var transaction = db.BeginTransaction();

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

            int rowsAffected = await db.ExecuteAsync(sqlUpdateGame, game, transaction);

            if (rowsAffected == 0)
            {
                transaction.Rollback();
                return false;
            }

            const string sqlDeleteHeroes = @"
                DELETE FROM GameHeroes 
                WHERE GameId = @Id";

            await db.ExecuteAsync(sqlDeleteHeroes, new { Id = game.Id }, transaction);

            if (game.Heroes != null && game.Heroes.Any())
            {
                const string sqlInsertHeroes = @"
                    INSERT INTO GameHeroes (
                        GameId, 
                        HeroId,
                        IsDead) 
                    VALUES (
                        @GameId, 
                        @HeroId,
                        @IsDead)";

                var batchHeroes = game.Heroes.Select(h => new
                {
                    GameId = game.Id,
                    HeroId = h.Id,
                    IsDead = h.IsDead
                });

                await db.ExecuteAsync(sqlInsertHeroes, batchHeroes, transaction);
            }

            transaction.Commit();
            return true;
        }

        /// <summary>
        /// Deletes a game by id.
        /// </summary>
        /// <param name="id">Identifier of the game to delete.</param>
        /// <returns><c>true</c> if a game was deleted; otherwise <c>false</c>.</returns>
        public async Task<bool> DeleteByIdAsync(int id)
        {
            using var db = new SqlConnection(_connectionString);

            const string sql = @"
                DELETE FROM Games 
                WHERE Id = @Id";

            int rowsAffected = await db.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0;
        }
    }
}
