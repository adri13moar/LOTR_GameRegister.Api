using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace LOTR_GameRegister.Application.Services.Implementations
{
    /// <summary>
    /// Provides business logic for game records.
    /// </summary>
    /// <param name="gameRepository">Data access for games.</param>
    /// <param name="heroRepository">Data access for heroes, used to resolve sphere counts.</param>
    /// <param name="logger">Logger for game operations.</param>
    public class GameService(IGameRepository gameRepository, IHeroRepository heroRepository, ILogger<GameService> logger) : IGameService
    {
        private readonly IGameRepository _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        private readonly IHeroRepository _heroRepository = heroRepository ?? throw new ArgumentNullException(nameof(heroRepository));
        private readonly ILogger<GameService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <inheritdoc />
        public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
            => (await _gameRepository.GetAllAsync()).Select(ToDto);

        /// <inheritdoc />
        public async Task<GameDto?> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return game is null ? null : ToDto(game);
        }

        /// <inheritdoc />
        public async Task<int> CreateGameAsync(CreateGameDto dto)
        {
            var game = ToEntity(dto);
            game.DatePlayed = DateOnly.FromDateTime(DateTime.UtcNow);
            await Recalculate(game);

            var gameId = await _gameRepository.CreateAsync(game);
            _logger.LogInformation("Created game {GameId} with {HeroCount} heroes.", gameId, game.Heroes.Count);
            return gameId;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateGameAsync(GameDto dto)
        {
            var game = ToEntity(dto);
            await Recalculate(game);

            var updated = await _gameRepository.UpdateAsync(game);
            _logger.LogInformation("Updated game {GameId}.", game.Id);
            return updated;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteGameAsync(int id)
        {
            var deleted = await _gameRepository.DeleteByIdAsync(id);
            _logger.LogInformation("Deleted game {GameId}.", id);
            return deleted;
        }

        private async Task Recalculate(Game game)
        {
            game.Recalculate();

            game.Spheres = game.Heroes.Count > 0
                ? (await _heroRepository.GetByIdsAsync(game.Heroes.Select(hero => hero.Id).ToList()))
                    .Select(hero => hero.SphereId)
                    .Distinct()
                    .Count()
                : 0;
        }

        private static Game ToEntity(CreateGameDto dto) => new()
        {
            QuestId = dto.QuestId,
            IsCampaignMode = dto.IsCampaignMode,
            DifficultyId = dto.DifficultyId,
            ResultId = dto.ResultId,
            ReasonForDefeatId = dto.ReasonForDefeatId,
            Notes = dto.Notes,
            Heroes = dto.Heroes
                .Select(hero => new Hero { Id = hero.HeroId, IsDead = hero.IsDead })
                .ToList()
        };

        private static Game ToEntity(GameDto dto) => new()
        {
            Id = dto.Id,
            QuestId = dto.QuestId,
            IsCampaignMode = dto.IsCampaignMode,
            DifficultyId = dto.DifficultyId,
            ResultId = dto.ResultId,
            ReasonForDefeatId = dto.ReasonForDefeatId,
            Notes = dto.Notes,
            Heroes = dto.Heroes
                .Select(hero => new Hero { Id = hero.HeroId, IsDead = hero.IsDead })
                .ToList()
        };

        private static GameDto ToDto(Game game) => new()
        {
            Id = game.Id,
            DatePlayed = game.DatePlayed,
            IsCampaignMode = game.IsCampaignMode,
            QuestId = game.QuestId,
            DifficultyId = game.DifficultyId,
            Spheres = game.Spheres,
            DeadHeroes = game.DeadHeroes,
            ResultId = game.ResultId,
            ReasonForDefeatId = game.ReasonForDefeatId,
            Notes = game.Notes,
            Heroes = (game.Heroes ?? [])
                .Select(hero => new GameHeroDto { HeroId = hero.Id, IsDead = hero.IsDead })
                .ToList()
        };
    }
}
