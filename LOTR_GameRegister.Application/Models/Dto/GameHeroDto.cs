namespace LOTR_GameRegister.Application.Models.Dto;

/// <summary>
/// A hero included in a game, along with whether that hero was defeated.
/// </summary>
public class GameHeroDto
{
    /// <summary>
    /// Identifier of the hero used in the game.
    /// </summary>
    public int HeroId { get; set; }

    /// <summary>
    /// Whether the hero died during that game.
    /// </summary>
    public bool IsDead { get; set; }
}
