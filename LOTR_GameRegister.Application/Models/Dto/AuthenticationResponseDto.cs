namespace LOTR_GameRegister.Application.Models.Dto
{
    /// <summary>
    /// Response returned by the API after a successful login.
    /// </summary>
    public class AuthenticationResponseDto
    {
        /// <summary>
        /// The authenticated user.
        /// </summary>
        public required UserDto User { get; set; }

        /// <summary>
        /// JWT issued to the user for subsequent authenticated requests.
        /// </summary>
        public required string Token { get; set; }
    }
}
