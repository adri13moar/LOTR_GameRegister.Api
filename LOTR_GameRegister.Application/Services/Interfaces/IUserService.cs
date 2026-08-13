using LOTR_GameRegister.Application.Models;
using LOTR_GameRegister.Application.Models.Dto;

namespace LOTR_GameRegister.Application.Services.Interfaces
{
    /// <summary>
    /// Provides business logic for user registration, authentication and listing.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all user accounts.
        /// </summary>
        /// <returns>All users in the register.</returns>
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        /// <summary>
        /// Registers a new user account, hashing the password and assigning the default player role.
        /// </summary>
        /// <param name="registrationDto">The registration payload.</param>
        /// <returns>The outcome of the registration attempt.</returns>
        Task<RegisterResult> RegisterAsync(UserRegistrationDto registrationDto);

        /// <summary>
        /// Authenticates a user and issues a JWT on success.
        /// </summary>
        /// <param name="loginDto">The login credentials.</param>
        /// <returns>An authentication response with the user and token, or <see langword="null"/> if the credentials are invalid.</returns>
        Task<AuthenticationResponseDto?> LoginAsync(UserLoginDto loginDto);
    }
}
