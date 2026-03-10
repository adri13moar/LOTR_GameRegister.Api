using LOTR_GameRegister.Api.Models.Entities;
using LOTR_GameRegister.Api.Models.Dto;
using LOTR_GameRegister.Api.Models.Enums;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services.Interfaces;
using BCrypt.Net;

namespace LOTR_GameRegister.Api.Services.Implementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Role = u.Role,
            CreatedAt = u.CreatedAt
        });
    }

    public async Task<bool> RegisterAsync(UserRegistrationDto dto)
    {
        if (await userRepository.ExistsAsync(dto.Username, dto.Email)) return false;

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var newUser = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = UserRole.Player.ToString(),
            CreatedAt = DateTime.UtcNow
        };

        var result = await userRepository.CreateAsync(newUser);
        return result > 0;
    }

    public async Task<UserDto?> LoginAsync(UserLoginDto loginDto)
    {
        var user = await userRepository.GetByUsernameAsync(loginDto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}