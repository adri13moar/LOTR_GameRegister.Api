using FluentAssertions;
using LOTR_GameRegister.Domain.Models.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LOTR_GameRegister.Tests;

[TestClass]
public class GameTests
{
    [TestMethod]
    public void Recalculate_WithNoHeroes_SetsDeadHeroesToZero()
    {
        var game = new Game();

        game.Recalculate();

        game.DeadHeroes.Should().Be(0);
    }

    [TestMethod]
    public void Recalculate_WithNullHeroes_SetsDeadHeroesToZero()
    {
        var game = new Game { Heroes = null! };

        game.Recalculate();

        game.DeadHeroes.Should().Be(0);
    }

    [TestMethod]
    public void Recalculate_WithNoDeadHeroes_SetsDeadHeroesToZero()
    {
        var game = new Game { Heroes = [new Hero(), new Hero()] };

        game.Recalculate();

        game.DeadHeroes.Should().Be(0);
    }

    [TestMethod]
    public void Recalculate_WithSomeDeadHeroes_CountsOnlyDeadOnes()
    {
        var game = new Game
        {
            Heroes = [new Hero { IsDead = true }, new Hero(), new Hero { IsDead = true }, new Hero()]
        };

        game.Recalculate();

        game.DeadHeroes.Should().Be(2);
    }

    [TestMethod]
    public void Recalculate_WithAllDeadHeroes_CountsEveryHero()
    {
        var game = new Game
        {
            Heroes = [new Hero { IsDead = true }, new Hero { IsDead = true }]
        };

        game.Recalculate();

        game.DeadHeroes.Should().Be(2);
    }
}
