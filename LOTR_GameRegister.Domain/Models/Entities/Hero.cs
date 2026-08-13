namespace LOTR_GameRegister.Domain.Models.Entities
{
    /// <summary>
    /// Represents a hero character card that can be used in a game.
    /// </summary>
    public class Hero
    {
        /// <summary>
        /// Unique identifier of the hero.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// English name of the hero.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Spanish name of the hero.
        /// </summary>
        public string Name_es { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the sphere the hero belongs to.
        /// </summary>
        public int SphereId { get; set; }

        /// <summary>
        /// Starting threat value of the hero.
        /// </summary>
        public int StartingThreat { get; set; }

        /// <summary>
        /// Whether the hero died in the game it is linked to (mapped from the GameHeroes join table).
        /// </summary>
        public bool IsDead { get; set; }

        /// <summary>
        /// Identifier of the hero card on RingsDB.
        /// </summary>
        public string? RingsDbId { get; set; }
    }
}
