namespace LOTR_GameRegister.Api.Models
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CycleId { get; set; }
        public decimal? CommunityDifficulty { get; set; }
    }
}