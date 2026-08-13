using System.Text.Json.Serialization;

namespace LOTR_GameRegister.Domain.Models.Entities
{
    /// <summary>
    /// Represents a quest (scenario) from the game, which is the adventure played in a game.
    /// </summary>
    public class Quest
    {
        /// <summary>
        /// Unique identifier of the quest.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// English name of the quest.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Spanish name of the quest.
        /// </summary>
        public string Name_es { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the cycle the quest belongs to.
        /// </summary>
        [JsonIgnore] public int CycleId { get; set; }

        /// <summary>
        /// Community-rated difficulty of the quest.
        /// </summary>
        public decimal? CommunityDifficulty { get; set; }

        /// <summary>
        /// Cycle the quest belongs to.
        /// </summary>
        public Cycle Cycle { get; set; } = null!;
    }
}
