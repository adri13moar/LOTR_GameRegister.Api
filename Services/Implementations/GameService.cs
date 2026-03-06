using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Repositories.Implementations;
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
            game.DeadHeroes = game.Heroes.Count(h => h.IsDead);

            var heroIds = game.Heroes.Select(h => h.Id).ToList();
            var heroesDetails = await _heroRepository.GetByIdsAsync(heroIds);
            game.Spheres = heroesDetails.Select(h => h.SphereId).Distinct().Count();

            return await _gameRepository.CreateAsync(game);
        }

        public async Task<bool> UpdateGameAsync(Game game)
            => await _gameRepository.UpdateAsync(game);

        public async Task<bool> DeleteGameAsync(int id)
            => await _gameRepository.DeleteByIdAsync(id);
    }
}