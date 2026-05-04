namespace LOTR_GameRegister.Api.Models.Dto
{
    public class AuthenticationResponseDto
    {
        public required UserDto User { get; set; }
        public required string Token { get; set; }
    }
}
