namespace LOTR_GameRegister.Application.Models;

/// <summary>
/// Outcome of a user registration attempt.
/// </summary>
public enum RegisterResult
{
    /// <summary>The user was created.</summary>
    Success,

    /// <summary>The username or email is already in use.</summary>
    UsernameOrEmailTaken
}
