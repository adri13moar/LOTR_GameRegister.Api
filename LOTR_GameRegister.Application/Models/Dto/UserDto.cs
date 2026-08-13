namespace LOTR_GameRegister.Application.Models.Dto
{
    /// <summary>
    /// A user account as returned by the API.
    /// </summary>
    public record UserDto
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Username used to log in.
        /// </summary>
        public string Username { get; init; } = string.Empty;

        /// <summary>
        /// Email address of the user.
        /// </summary>
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Role of the user (e.g. "Admin" or "Player"), used for authorization.
        /// </summary>
        public string Role { get; init; } = string.Empty;

        /// <summary>
        /// Date and time (UTC) at which the account was created.
        /// </summary>
        public DateTime CreatedAt { get; init; }
    }
}
