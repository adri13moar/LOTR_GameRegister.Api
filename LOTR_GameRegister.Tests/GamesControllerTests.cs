using FluentAssertions;
using LOTR_GameRegister.Api.Controllers;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Globalization;

namespace LOTR_GameRegister.Tests;

[TestClass]
public class GamesControllerTests
{
    private readonly Mock<IGameService> _service = new();

    private GamesController CreateController() => new(_service.Object);

    private static GameDto CreateGame() => new()
    {
        Id = 1,
        DatePlayed = new DateOnly(2024, 1, 15),
        QuestId = 1,
        DifficultyId = 2,
        ResultId = 1,
        Spheres = 2,
        DeadHeroes = 1
    };

    [TestMethod]
    public async Task GetAll_ReturnsOkWithGames()
    {
        var games = new List<GameDto> { CreateGame() };
        _service.Setup(s => s.GetAllGamesAsync()).ReturnsAsync(games);

        var result = await CreateController().GetAll();

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeSameAs(games);
    }

    [TestMethod]
    public async Task GetById_WhenFound_ReturnsOkWithGame()
    {
        var game = CreateGame();
        _service.Setup(s => s.GetGameByIdAsync(1)).ReturnsAsync(game);

        var result = await CreateController().GetById(1);

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeSameAs(game);
    }

    [TestMethod]
    public async Task GetById_WhenNotFound_ReturnsEnglishMessage()
    {
        _service.Setup(s => s.GetGameByIdAsync(99)).ReturnsAsync((GameDto?)null);

        var original = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

            var result = await CreateController().GetById(99);

            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("Game with ID 99 not found.");
        }
        finally
        {
            CultureInfo.CurrentUICulture = original;
        }
    }

    [TestMethod]
    public async Task GetById_WhenNotFound_ReturnsSpanishMessage()
    {
        var spanish = CultureTestHelper.TryCreate("es-ES");
        if (spanish is null)
        {
            Assert.Inconclusive("Culture 'es-ES' is not available in this environment (globalization invariant mode).");
            return;
        }

        _service.Setup(s => s.GetGameByIdAsync(99)).ReturnsAsync((GameDto?)null);

        var original = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentUICulture = spanish;

            var result = await CreateController().GetById(99);

            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().Be("No se encontró la partida con ID 99.");
        }
        finally
        {
            CultureInfo.CurrentUICulture = original;
        }
    }

    [TestMethod]
    public async Task Create_ReturnsCreatedAtActionPointingToGetById()
    {
        var created = CreateGame();
        _service.Setup(s => s.CreateGameAsync(It.IsAny<CreateGameDto>())).ReturnsAsync(7);
        _service.Setup(s => s.GetGameByIdAsync(7)).ReturnsAsync(created);

        var result = await CreateController().Create(new CreateGameDto { QuestId = 1, DifficultyId = 2, ResultId = 1 });

        var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.ActionName.Should().Be(nameof(GamesController.GetById));
        createdResult.RouteValues!["id"].Should().Be(7);
        createdResult.Value.Should().BeSameAs(created);
    }

    [TestMethod]
    public async Task Update_WhenUrlAndBodyIdsMismatch_ReturnsBadRequest()
    {
        var result = await CreateController().Update(1, new GameDto { Id = 2, QuestId = 1, DifficultyId = 2, ResultId = 1 });

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [TestMethod]
    public async Task Update_WhenGameExists_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateGameAsync(It.IsAny<GameDto>())).ReturnsAsync(true);

        var result = await CreateController().Update(1, CreateGame());

        result.Should().BeOfType<NoContentResult>();
    }

    [TestMethod]
    public async Task Update_WhenGameMissing_ReturnsNotFound()
    {
        _service.Setup(s => s.UpdateGameAsync(It.IsAny<GameDto>())).ReturnsAsync(false);

        var result = await CreateController().Update(1, CreateGame());

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [TestMethod]
    public async Task Delete_WhenDeleted_ReturnsNoContent()
    {
        _service.Setup(s => s.DeleteGameAsync(1)).ReturnsAsync(true);

        var result = await CreateController().Delete(1);

        result.Should().BeOfType<NoContentResult>();
    }

    [TestMethod]
    public async Task Delete_WhenMissing_ReturnsNotFound()
    {
        _service.Setup(s => s.DeleteGameAsync(1)).ReturnsAsync(false);

        var result = await CreateController().Delete(1);

        result.Should().BeOfType<NotFoundObjectResult>();
    }
}
