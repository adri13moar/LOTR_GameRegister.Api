namespace LOTR_GameRegister.Api.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime DatePlayed { get; set; }
        public bool IsCampaignMode { get; set; }
        public int QuestId { get; set; }
        public int DifficultyId { get; set; }
        public int Spheres { get; set; }
        public int DeadHeroes { get; set; }
        public int ResultId { get; set; }
        public int? ReasonForDefeatId { get; set; }
        public string? Notes { get; set; }
        public List<Hero> Heroes { get; set; } = new List<Hero>();
    }
}
