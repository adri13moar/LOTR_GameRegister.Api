namespace LOTR_GameRegister.Api.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sphere { get; set; } = string.Empty;
        public int StartingThreat { get; set; }
    }
}