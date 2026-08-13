namespace LOTR_GameRegister.Domain.Models.Entities
{    
    /// <summary>
    /// Represents a product cycle (or saga) grouping a set of quests, e.g. "Shadows of Mirkwood".
    /// </summary>
    public class Cycle
    {
        /// <summary>
        /// Unique identifier of the cycle.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// English name of the cycle.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Spanish name of the cycle.
        /// </summary>
        public string Name_es { get; set; } = string.Empty;

        /// <summary>
        /// Category of the cycle, e.g. "Official", "Hobbit Saga", "LOTR Saga", "PoD" or "ALEP".
        /// </summary>
        public string Category { get; set; } = string.Empty;
    }
}
