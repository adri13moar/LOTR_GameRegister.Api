using LOTR_GameRegister.Api.Models.Dto;

namespace LOTR_GameRegister.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> RegisterAsync(UserRegistrationDto registrationDto);
    }
}
