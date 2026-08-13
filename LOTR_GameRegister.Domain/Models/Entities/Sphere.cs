namespace LOTR_GameRegister.Domain.Models.Entities
{
    /// <summary>
    /// Represents a sphere (resource type) a hero can belong to, e.g. "Leadership", "Tactics", "Spirit" or "Lore".
    /// </summary>
    public class Sphere
    {
        /// <summary>
        /// Unique identifier of the sphere.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// English name of the sphere.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Spanish name of the sphere.
        /// </summary>
        public string Name_es { get; set; } = string.Empty;
    }
}
