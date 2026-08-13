using FluentAssertions;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Implementations;
using LOTR_GameRegister.Domain.Models.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LOTR_GameRegister.Tests;

[TestClass]
public class GameServiceTests
{
    private readonly Mock<IGameRepository> _gameRepository = new();
    private readonly Mock<IHeroRepository> _heroRepository = new();
    private readonly Mock<ILogger<GameService>> _logger = new();

    private GameService CreateService() => new(_gameRepository.Object, _heroRepository.Object, _logger.Object);

    [TestMethod]
    public async Task GetAllGamesAsync_MapsEntitiesToDtos()
    {
        _gameRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Game>
        {
            new()
            {
                Id = 1,
                DatePlayed = new DateOnly(2024, 1, 15),
                QuestId = 1,
                DifficultyId = 2,
                Spheres = 3,
                DeadHeroes = 1,
                ResultId = 1,
                Heroes = new List<Hero> { new() { Id = 7, IsDead = true } }
            }
        });

        var games = (await CreateService().GetAllGamesAsync()).ToList();

        games.Should().ContainSingle();
        games[0].Id.Should().Be(1);
        games[0].DatePlayed.Should().Be(new DateOnly(2024, 1, 15));
        games[0].DeadHeroes.Should().Be(1);
        games[0].Heroes.Should().ContainSingle().Which.HeroId.Should().Be(7);
    }

    [TestMethod]
    public async Task GetGameByIdAsync_WhenNotFound_ReturnsNull()
    {
        _gameRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Game?)null);

        var game = await CreateService().GetGameByIdAsync(99);

        game.Should().BeNull();
    }

    [TestMethod]
    public async Task CreateGameAsync_SetsTodayUtcDate_AndReturnsRepositoryId()
    {
        _gameRepository.Setup(r => r.CreateAsync(It.IsAny<Game>())).ReturnsAsync(42);

        var id = await CreateService().CreateGameAsync(new CreateGameDto
        {
            QuestId = 1,
            DifficultyId = 2,
            ResultId = 1
        });

        id.Should().Be(42);
        _gameRepository.Verify(r => r.CreateAsync(It.Is<Game>(g =>
            g.DatePlayed == DateOnly.FromDateTime(DateTime.UtcNow))), Times.Once);
    }

    [TestMethod]
    public async Task CreateGameAsync_RecalculatesDeadHeroes_FromHeroes()
    {
        _gameRepository.Setup(r => r.CreateAsync(It.IsAny<Game>())).ReturnsAsync(1);

        var dto = new CreateGameDto { QuestId = 1, DifficultyId = 2, ResultId = 1 };
        dto.Heroes.AddRange(new[]
        {
            new GameHeroDto { HeroId = 1, IsDead = true },
            new GameHeroDto { HeroId = 2, IsDead = false },
            new GameHeroDto { HeroId = 3, IsDead = false }
        });

        await CreateService().CreateGameAsync(dto);

        _gameRepository.Verify(r => r.CreateAsync(It.Is<Game>(g => g.DeadHeroes == 1)), Times.Once);
    }

    [TestMethod]
    public async Task CreateGameAsync_RecalculatesSpheres_FromDistinctHeroSphereIds()
    {
        _heroRepository.Setup(r => r.GetByIdsAsync(It.IsAny<List<int>>())).ReturnsAsync(new List<Hero>
        {
            new() { Id = 1, SphereId = 2 },
            new() { Id = 2, SphereId = 2 },
            new() { Id = 3, SphereId = 3 }
        });
        _gameRepository.Setup(r => r.CreateAsync(It.IsAny<Game>())).ReturnsAsync(1);

        var dto = new CreateGameDto { QuestId = 1, DifficultyId = 2, ResultId = 1 };
        dto.Heroes.AddRange(new[]
        {
            new GameHeroDto { HeroId = 1 },
            new GameHeroDto { HeroId = 2 },
            new GameHeroDto { HeroId = 3 }
        });

        await CreateService().CreateGameAsync(dto);

        _gameRepository.Verify(r => r.CreateAsync(It.Is<Game>(g =>
            g.Spheres == 2 && g.DeadHeroes == 0)), Times.Once);
    }

    [TestMethod]
    public async Task UpdateGameAsync_ReturnsRepositoryResult()
    {
        _gameRepository.Setup(r => r.UpdateAsync(It.IsAny<Game>())).ReturnsAsync(true);

        var updated = await CreateService().UpdateGameAsync(new GameDto
        {
            Id = 5,
            QuestId = 1,
            DifficultyId = 2,
            ResultId = 1
        });

        updated.Should().BeTrue();
        _gameRepository.Verify(r => r.UpdateAsync(It.Is<Game>(g => g.Id == 5)), Times.Once);
    }

    [TestMethod]
    public async Task DeleteGameAsync_ReturnsRepositoryResult()
    {
        _gameRepository.Setup(r => r.DeleteByIdAsync(It.IsAny<int>())).ReturnsAsync(true);

        var deleted = await CreateService().DeleteGameAsync(5);

        deleted.Should().BeTrue();
        _gameRepository.Verify(r => r.DeleteByIdAsync(5), Times.Once);
    }

    [TestMethod]
    public void Constructor_WithNullGameRepository_ThrowsArgumentNullException()
    {
        var act = () => new GameService(null!, _heroRepository.Object, _logger.Object);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Constructor_WithNullHeroRepository_ThrowsArgumentNullException()
    {
        var act = () => new GameService(_gameRepository.Object, null!, _logger.Object);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        var act = () => new GameService(_gameRepository.Object, _heroRepository.Object, null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public async Task GetAllGamesAsync_WhenEmpty_ReturnsEmpty()
    {
        _gameRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Game>());

        var games = await CreateService().GetAllGamesAsync();

        games.Should().BeEmpty();
    }

    [TestMethod]
    public async Task CreateGameAsync_WithNoHeroes_SetsDerivedValuesToZero()
    {
        _gameRepository.Setup(r => r.CreateAsync(It.IsAny<Game>())).ReturnsAsync(1);

        var id = await CreateService().CreateGameAsync(new CreateGameDto
        {
            QuestId = 1,
            DifficultyId = 2,
            ResultId = 1
        });

        id.Should().Be(1);
        _gameRepository.Verify(r => r.CreateAsync(It.Is<Game>(g => g.DeadHeroes == 0 && g.Spheres == 0)), Times.Once);
        _heroRepository.Verify(r => r.GetByIdsAsync(It.IsAny<List<int>>()), Times.Never);
    }
}
