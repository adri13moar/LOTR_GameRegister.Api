namespace LOTR_GameRegister.Api.Models
{ 
    public class Game
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public bool IsCampaignMode { get; set; }
        public int DifficultyId { get; set; }
        public int Hero1Id { get; set; }
        public int? Hero2Id { get; set; }
        public int? Hero3Id { get; set; }
        public int? Hero4Id { get; set; }
        public int Spheres { get; set; }
        public int DeadHeroes { get; set; }
        public int ResultId { get; set; }
        public int? ReasonForDefeatId { get; set; }
        public DateTime DatePlayed { get; set; }
        public string? Notes { get; set; }
    }
}
