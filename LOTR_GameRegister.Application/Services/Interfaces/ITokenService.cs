using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Services.Interfaces;

/// <summary>
/// Generates JSON Web Tokens (JWT) for authenticated users.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Creates a signed JWT for the given user.
    /// </summary>
    /// <param name="user">The authenticated user to issue the token for.</param>
    /// <returns>A JWT string with the user's identity and role claims.</returns>
    string GenerateJwtToken(User user);
}
