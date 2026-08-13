using System.ComponentModel.DataAnnotations;

namespace LOTR_GameRegister.Application.Models.Dto;

/// <summary>
/// Payload used to register a new user account.
/// </summary>
public record UserRegistrationDto
{
    /// <summary>
    /// Desired username (6 to 50 characters).
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Username { get; init; } = string.Empty;

    /// <summary>
    /// Email address of the user, validated as a well-formed address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Raw password (6 to 100 characters), hashed with BCrypt on registration.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; init; } = string.Empty;
}