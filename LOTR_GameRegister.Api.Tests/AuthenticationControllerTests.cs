using FluentAssertions;
using LOTR_GameRegister.Api.Controllers;
using LOTR_GameRegister.Application.Models;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LOTR_GameRegister.Api.Tests;

[TestClass]
public class AuthenticationControllerTests
{
    private readonly Mock<IUserService> _service = new();

    private AuthenticationController CreateController() => new(_service.Object);

    private static AuthenticationResponseDto CreateResponse() => new()
    {
        Token = "jwt-token",
        User = new UserDto { Id = 1, Username = "aragorn", Email = "aragorn@gondor.test", Role = "Player" }
    };

    private static UserLoginDto CreateLogin() => new() { Username = "aragorn", Password = "Anduril123!" };

    private static UserRegistrationDto CreateRegistration() => new()
    {
        Username = "aragorn",
        Email = "aragorn@gondor.test",
        Password = "Anduril123!"
    };

    [TestMethod]
    public async Task Login_WithValidCredentials_ReturnsOkWithResponse()
    {
        var response = CreateResponse();
        _service.Setup(s => s.LoginAsync(It.IsAny<UserLoginDto>())).ReturnsAsync(response);

        var result = await CreateController().Login(CreateLogin());

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeSameAs(response);
    }

    [TestMethod]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        _service.Setup(s => s.LoginAsync(It.IsAny<UserLoginDto>())).ReturnsAsync((AuthenticationResponseDto?)null);

        var result = await CreateController().Login(CreateLogin());

        result.Should().BeOfType<UnauthorizedObjectResult>();
    }

    [TestMethod]
    public async Task Register_WhenSuccessful_ReturnsOk()
    {
        _service.Setup(s => s.RegisterAsync(It.IsAny<UserRegistrationDto>())).ReturnsAsync(RegisterResult.Success);

        var result = await CreateController().Register(CreateRegistration());

        result.Should().BeOfType<OkObjectResult>();
    }

    [TestMethod]
    public async Task Register_WhenCredentialsTaken_ReturnsBadRequest()
    {
        _service.Setup(s => s.RegisterAsync(It.IsAny<UserRegistrationDto>()))
            .ReturnsAsync(RegisterResult.UsernameOrEmailTaken);

        var result = await CreateController().Register(CreateRegistration());

        result.Should().BeOfType<BadRequestObjectResult>();
    }
}
