using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;

namespace LOTR_GameRegister.Api.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IHeroRepository _heroRepository;

        public GameService(IGameRepository gameRepository, IHeroRepository heroRepository)
        {
            _gameRepository = gameRepository;
            _heroRepository = heroRepository;
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
            => await _gameRepository.GetAllAsync();

        public async Task<Game?> GetGameByIdAsync(int id)
            => await _gameRepository.GetByIdAsync(id);

        public async Task<int> CreateGameAsync(Game game)
        {
            game.DatePlayed = DateOnly.FromDateTime(DateTime.Now);
            await RebuildGameCalculations(game);

            return await _gameRepository.CreateAsync(game);
        }

        public async Task<bool> UpdateGameAsync(Game game)
        {
            await RebuildGameCalculations(game);

            return await _gameRepository.UpdateAsync(game);
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            return await _gameRepository.DeleteByIdAsync(id);
        }

        private async Task RebuildGameCalculations(Game game)
        {
            game.DeadHeroes = game.Heroes?.Count(heroe => heroe.IsDead) ?? 0;

            if (game.Heroes != null && game.Heroes.Any())
            {
                var ids = game.Heroes.Select(heroe => heroe.Id).ToList();
                var heroesDetails = await _heroRepository.GetByIdsAsync(ids);

                game.Spheres = heroesDetails.Select(heroe => heroe.SphereId).Distinct().Count();
            }

            else
            {
                game.Spheres = 0;
            }
        }
    }
}