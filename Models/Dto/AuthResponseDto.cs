namespace LOTR_GameRegister.Api.Models.Dto
{
    public class AuthResponseDto
    {
        public required UserDto User { get; set; }
        public required string Token { get; set; }
    }
}
