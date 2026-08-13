using FluentAssertions;
using LOTR_GameRegister.Domain.Models.Entities;
using LOTR_GameRegister.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LOTR_GameRegister.Api.Tests;

[TestClass]
public class TokenServiceTests
{
    private const string Key = "Test_Signing_Key_At_Least_32_Characters_Long!";
    private const string Issuer = "LOTR_Test_Issuer";
    private const string Audience = "LOTR_Test_Audience";

    private static Mock<IConfiguration> BuildConfig(string durationInMinutes = "60")
    {
        var config = new Mock<IConfiguration>();
        config.Setup(c => c["Jwt:Key"]).Returns(Key);
        config.Setup(c => c["Jwt:Issuer"]).Returns(Issuer);
        config.Setup(c => c["Jwt:Audience"]).Returns(Audience);
        config.Setup(c => c["Jwt:DurationInMinutes"]).Returns(durationInMinutes);
        return config;
    }

    private static User CreateUser() => new()
    {
        Id = 1,
        Username = "aragorn",
        Email = "aragorn@gondor.test",
        Role = "Player"
    };

    [TestMethod]
    public void Constructor_WithNullConfig_ThrowsArgumentNullException()
    {
        var act = () => new TokenService(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void GenerateJwtToken_ReturnsWellFormedJwt()
    {
        var service = new TokenService(BuildConfig().Object);

        var token = service.GenerateJwtToken(CreateUser());

        token.Should().NotBeNullOrWhiteSpace();
        token.Split('.').Should().HaveCount(3);
    }

    [TestMethod]
    public void GenerateJwtToken_IssuesTokenThatValidatesWithTheSameKey()
    {
        var service = new TokenService(BuildConfig().Object);

        var token = service.GenerateJwtToken(CreateUser());

        var handler = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)),
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        var principal = handler.ValidateToken(token, parameters, out _);

        principal.Should().NotBeNull();
        principal.FindFirstValue(ClaimTypes.NameIdentifier).Should().Be("1");
        principal.FindFirstValue(ClaimTypes.Name).Should().Be("aragorn");
        principal.FindFirstValue(ClaimTypes.Email).Should().Be("aragorn@gondor.test");
        principal.FindFirstValue(ClaimTypes.Role).Should().Be("Player");
    }

    [TestMethod]
    public void GenerateJwtToken_SetsExpirationFromDurationInMinutes()
    {
        var service = new TokenService(BuildConfig("60").Object);

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(service.GenerateJwtToken(CreateUser()));

        jwt.ValidTo.Should().BeCloseTo(DateTime.UtcNow.AddMinutes(60), TimeSpan.FromMinutes(1));
    }

    [TestMethod]
    public void GenerateJwtToken_UsesConfiguredIssuerAndAudience()
    {
        var service = new TokenService(BuildConfig().Object);

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(service.GenerateJwtToken(CreateUser()));

        jwt.Issuer.Should().Be(Issuer);
        jwt.Audiences.Should().Contain(Audience);
    }
}
