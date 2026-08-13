using FluentAssertions;
using LOTR_GameRegister.Application.Models;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Repositories.Interfaces;
using LOTR_GameRegister.Application.Services.Implementations;
using LOTR_GameRegister.Application.Services.Interfaces;
using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Domain.Models.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LOTR_GameRegister.Tests;

[TestClass]
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<ITokenService> _tokenService = new();
    private readonly Mock<ILogger<UserService>> _logger = new();

    private UserService CreateService() => new(_userRepository.Object, _tokenService.Object, _logger.Object);

    [TestMethod]
    public async Task RegisterAsync_WhenCredentialsFree_ReturnsSuccess_AndHashesPassword()
    {
        _userRepository.Setup(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
        _userRepository.Setup(r => r.CreateAsync(It.IsAny<User>())).ReturnsAsync(1);

        var result = await CreateService().RegisterAsync(new UserRegistrationDto
        {
            Username = "aragorn",
            Email = "aragorn@gondor.test",
            Password = "Anduril123!"
        });

        result.Should().Be(RegisterResult.Success);
        _userRepository.Verify(r => r.CreateAsync(It.Is<User>(u =>
            u.Username == "aragorn" &&
            u.Role == UserRole.Player.ToString() &&
            BCrypt.Net.BCrypt.Verify("Anduril123!", u.PasswordHash))), Times.Once);
    }

    [TestMethod]
    public async Task RegisterAsync_WhenCredentialsTaken_ReturnsUsernameOrEmailTaken_AndDoesNotCreate()
    {
        _userRepository.Setup(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

        var result = await CreateService().RegisterAsync(new UserRegistrationDto
        {
            Username = "aragorn",
            Email = "aragorn@gondor.test",
            Password = "Anduril123!"
        });

        result.Should().Be(RegisterResult.UsernameOrEmailTaken);
        _userRepository.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Never);
    }

    [TestMethod]
    public async Task LoginAsync_WithValidCredentials_ReturnsUserAndToken()
    {
        var user = new User
        {
            Id = 1,
            Username = "aragorn",
            Email = "aragorn@gondor.test",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Anduril123!"),
            Role = "Player"
        };
        _userRepository.Setup(r => r.GetByUsernameAsync("aragorn")).ReturnsAsync(user);
        _tokenService.Setup(t => t.GenerateJwtToken(user)).Returns("jwt-token");

        var result = await CreateService().LoginAsync(new UserLoginDto
        {
            Username = "aragorn",
            Password = "Anduril123!"
        });

        result.Should().NotBeNull();
        result!.Token.Should().Be("jwt-token");
        result.User.Username.Should().Be("aragorn");
        result.User.Role.Should().Be("Player");
    }

    [TestMethod]
    public async Task LoginAsync_WithWrongPassword_ReturnsNull()
    {
        var user = new User
        {
            Id = 1,
            Username = "aragorn",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("correct-horse")
        };
        _userRepository.Setup(r => r.GetByUsernameAsync("aragorn")).ReturnsAsync(user);

        var result = await CreateService().LoginAsync(new UserLoginDto
        {
            Username = "aragorn",
            Password = "wrong-password"
        });

        result.Should().BeNull();
    }

    [TestMethod]
    public async Task LoginAsync_WithUnknownUser_ReturnsNull()
    {
        _userRepository.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

        var result = await CreateService().LoginAsync(new UserLoginDto
        {
            Username = "unknown",
            Password = "whatever"
        });

        result.Should().BeNull();
    }

    [TestMethod]
    public async Task GetAllUsersAsync_MapsEntitiesToDtos()
    {
        _userRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>
        {
            new()
            {
                Id = 1,
                Username = "aragorn",
                Email = "aragorn@gondor.test",
                Role = "Admin",
                CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc)
            }
        });

        var users = (await CreateService().GetAllUsersAsync()).ToList();

        users.Should().ContainSingle();
        users[0].Username.Should().Be("aragorn");
        users[0].Email.Should().Be("aragorn@gondor.test");
        users[0].Role.Should().Be("Admin");
        users[0].CreatedAt.Should().Be(new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc));
    }

    [TestMethod]
    public void Constructor_WithNullUserRepository_ThrowsArgumentNullException()
    {
        var act = () => new UserService(null!, _tokenService.Object, _logger.Object);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Constructor_WithNullTokenService_ThrowsArgumentNullException()
    {
        var act = () => new UserService(_userRepository.Object, null!, _logger.Object);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        var act = () => new UserService(_userRepository.Object, _tokenService.Object, null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public async Task GetAllUsersAsync_WhenEmpty_ReturnsEmpty()
    {
        _userRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>());

        var users = await CreateService().GetAllUsersAsync();

        users.Should().BeEmpty();
    }

    [TestMethod]
    public async Task RegisterAsync_WhenRepositoryFailsToCreate_ReturnsUsernameOrEmailTaken()
    {
        _userRepository.Setup(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
        _userRepository.Setup(r => r.CreateAsync(It.IsAny<User>())).ReturnsAsync(0);

        var result = await CreateService().RegisterAsync(new UserRegistrationDto
        {
            Username = "aragorn",
            Email = "aragorn@gondor.test",
            Password = "Anduril123!"
        });

        result.Should().Be(RegisterResult.UsernameOrEmailTaken);
    }
}
