using FluentAssertions;
using LOTR_GameRegister.Api.Controllers;
using LOTR_GameRegister.Application.Models.Dto;
using LOTR_GameRegister.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LOTR_GameRegister.Tests;

[TestClass]
public class UsersControllerTests
{
    private readonly Mock<IUserService> _service = new();

    private UsersController CreateController() => new(_service.Object);

    [TestMethod]
    public async Task GetAll_ReturnsOkWithUsers()
    {
        var users = new List<UserDto>
        {
            new() { Id = 1, Username = "aragorn", Email = "aragorn@gondor.test", Role = "Admin" }
        };
        _service.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

        var result = await CreateController().GetAll();

        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeSameAs(users);
    }
}
