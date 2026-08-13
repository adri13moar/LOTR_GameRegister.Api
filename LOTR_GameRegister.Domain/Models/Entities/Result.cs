namespace LOTR_GameRegister.Domain.Models.Entities
{
    /// <summary>
    /// Represents the outcome of a game, e.g. "Win", "Loss" or "Surrender".
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Unique identifier of the result.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// English name of the result.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Spanish name of the result.
        /// </summary>
        public string Name_es { get; set; } = string.Empty;
    }
}
