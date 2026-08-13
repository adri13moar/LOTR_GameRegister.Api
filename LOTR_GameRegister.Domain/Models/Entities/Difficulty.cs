namespace LOTR_GameRegister.Domain.Models.Entities
{
    /// <summary>
    /// Represents a difficulty level a quest can be played at, e.g. "Easy", "Normal" or "Nightmare".
    /// </summary>
    public class Difficulty
    {
        /// <summary>
        /// Unique identifier of the difficulty level.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// English name of the difficulty level.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Spanish name of the difficulty level.
        /// </summary>
        public string Name_es { get; set; } = string.Empty;
    }
}
