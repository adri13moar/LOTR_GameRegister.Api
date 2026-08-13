using LOTR_GameRegister.Domain.Models.Entities;

namespace LOTR_GameRegister.Application.Repositories.Interfaces
{
    /// <summary>
    /// Provides data access for user accounts.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates a new user account.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>The id of the newly created user.</returns>
        Task<int> CreateAsync(User user);

        /// <summary>
        /// Retrieves all user accounts.
        /// </summary>
        /// <returns>All users in the register.</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Retrieves a single user by username.
        /// </summary>
        /// <param name="username">Username to look up.</param>
        /// <returns>The matching user, or <see langword="null"/> if not found.</returns>
        Task<User?> GetByUsernameAsync(string username);

        /// <summary>
        /// Retrieves a single user by email.
        /// </summary>
        /// <param name="email">Email address to look up.</param>
        /// <returns>The matching user, or <see langword="null"/> if not found.</returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Checks whether a user with the given username or email already exists.
        /// </summary>
        /// <param name="username">Username to check.</param>
        /// <param name="email">Email address to check.</param>
        /// <returns><see langword="true"/> if either the username or the email is already in use.</returns>
        Task<bool> ExistsAsync(string username, string email);
    }
}
