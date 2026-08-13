using BCrypt.Net;
using LOTR_GameRegister.Application.Models;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Interfaces;
using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Domain.Models.Enums;
using Microsoft.Extensions.Logging;

namespace LOTR_GameRegister.Application.Services.Implementations;

/// <summary>
/// Orchestrates user registration, authentication and user listing.
/// </summary>
/// <param name="userRepository">Data access for user accounts.</param>
/// <param name="tokenService">Issues JWTs for authenticated users.</param>
/// <param name="logger">Logger for user operations.</param>
public class UserService(IUserRepository userRepository, ITokenService tokenService, ILogger<UserService> logger) : IUserService
{
    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<RegisterResult> RegisterAsync(UserRegistrationDto dto)
    {
        if (await userRepository.ExistsAsync(dto.Username, dto.Email))
        {
            logger.LogWarning("Registration rejected for username {Username} or email {Email}: already in use.", dto.Username, dto.Email);
            return RegisterResult.UsernameOrEmailTaken;
        }

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
        logger.LogInformation("Registered new user {Username}.", dto.Username);
        return result > 0 ? RegisterResult.Success : RegisterResult.UsernameOrEmailTaken;
    }

    /// <inheritdoc />
    public async Task<AuthenticationResponseDto?> LoginAsync(UserLoginDto loginDto)
    {
        var user = await userRepository.GetByUsernameAsync(loginDto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            logger.LogWarning("Failed login attempt for username {Username}.", loginDto.Username);
            return null;
        }

        var token = tokenService.GenerateJwtToken(user);
        logger.LogInformation("User {Username} logged in.", user.Username);

        return new AuthenticationResponseDto
        {
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            },
            Token = token
        };
    }
}
