using System.ComponentModel.DataAnnotations;

namespace LOTR_GameRegister.Application.Models.Dto;

/// <summary>
/// Payload used to create a new game record.
/// </summary>
public class CreateGameDto
{
    /// <summary>
    /// Identifier of the quest (scenario) played in the game.
    /// </summary>
    [Required]
    public int QuestId { get; set; }

    /// <summary>
    /// Whether the game was played as part of a campaign mode.
    /// </summary>
    public bool IsCampaignMode { get; set; }

    /// <summary>
    /// Identifier of the difficulty level the game was played at.
    /// </summary>
    [Required]
    public int DifficultyId { get; set; }

    /// <summary>
    /// Identifier of the game outcome (win, loss or surrender).
    /// </summary>
    [Required]
    public int ResultId { get; set; }

    /// <summary>
    /// Identifier of the reason for defeat, when the game was lost.
    /// </summary>
    public int? ReasonForDefeatId { get; set; }

    /// <summary>
    /// Free-text notes about the game.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Heroes used in the game, along with whether each hero died.
    /// </summary>
    public List<GameHeroDto> Heroes { get; set; } = [];
}
