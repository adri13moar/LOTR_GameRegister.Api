using System.ComponentModel.DataAnnotations;

namespace LOTR_GameRegister.Api.Models.Dto;

public record UserRegistrationDto
{
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Username { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; init; } = string.Empty;
}