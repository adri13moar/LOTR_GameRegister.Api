namespace LOTR_GameRegister.Api.Models.Entities
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Name_es { get; set; } = string.Empty;
        public int SphereId { get; set; }
        public int StartingThreat { get; set; }
        public bool IsDead { get; set; }
        public string? RingsDbId { get; set; }
    }
}