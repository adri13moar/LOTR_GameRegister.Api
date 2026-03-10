namespace LOTR_GameRegister.Api.Models.Dto
{
    public class UserLoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
