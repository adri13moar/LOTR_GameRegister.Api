namespace LOTR_GameRegister.Api.Models.Entities
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Name_es { get; set; } = string.Empty;
        public int CycleId { get; set; }
        public decimal? CommunityDifficulty { get; set; }
        public Cycle Cycle { get; set; } = null!;
    }
}