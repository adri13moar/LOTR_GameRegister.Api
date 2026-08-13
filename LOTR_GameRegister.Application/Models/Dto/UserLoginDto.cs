using System.ComponentModel.DataAnnotations;

namespace LOTR_GameRegister.Application.Models.Dto
{
    /// <summary>
    /// Payload used to authenticate an existing user.
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Username of the user trying to log in.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public required string Username { get; set; }

        /// <summary>
        /// Raw password of the user, verified against the stored BCrypt hash on login.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public required string Password { get; set; }
    }
}
