namespace LOTR_GameRegister.Domain.Models.Entities
{
    /// <summary>
    /// Represents the reason a game was lost, e.g. "Heroes Defeated" or "Threat Elimination".
    /// </summary>
    public class ReasonForDefeat
    {
        /// <summary>
        /// Unique identifier of the reason for defeat.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// English name of the reason for defeat.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Spanish name of the reason for defeat.
        /// </summary>
        public string Name_es { get; set; } = string.Empty;
    }
}
