using FluentAssertions;
using LOTR_GameRegister.Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LOTR_GameRegister.Api.Tests;

[TestClass]
public class ApiExceptionHandlerTests
{
    [TestMethod]
    public async Task TryHandleAsync_ReturnsTrueAndWritesProblemDetails()
    {
        var context = new DefaultHttpContext();
        context.Request.Method = HttpMethods.Get;
        context.Request.Path = "/api/games";
        context.Response.Body = new MemoryStream();
        var logger = new Mock<ILogger<ApiExceptionHandler>>();

        var handler = new ApiExceptionHandler(logger.Object);

        var handled = await handler.TryHandleAsync(
            context, new InvalidOperationException("boom"), CancellationToken.None);

        handled.Should().BeTrue();
        context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body);
        var body = await reader.ReadToEndAsync();

        body.Should().Contain("\"status\":500");
        body.Should().Contain("An unexpected error occurred while processing the request.");

        logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
            Times.Once);
    }
}
