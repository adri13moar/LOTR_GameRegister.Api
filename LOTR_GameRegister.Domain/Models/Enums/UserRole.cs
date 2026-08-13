namespace LOTR_GameRegister.Domain.Models.Enums
{
    /// <summary>
    /// Roles a user can have in the register.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Administrator role with full access to the register.
        /// </summary>
        Admin,

        /// <summary>
        /// Regular player role with access to game logging features.
        /// </summary>
        Player
    }
}
