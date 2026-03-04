using chapelhilldotnet.web.Services;
using Microsoft.JSInterop;
using Moq;
using Xunit;

namespace chapelhilldotnet.Tests.Services;

public class SimpleAuthenticationServiceTests
{
    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsTrue()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new SimpleAuthenticationService(mockJsRuntime.Object);

        // Act
        var result = await service.LoginAsync("admin", "Chapel2024!");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidUsername_ReturnsFalse()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new SimpleAuthenticationService(mockJsRuntime.Object);

        // Act
        var result = await service.LoginAsync("wronguser", "Chapel2024!");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidPassword_ReturnsFalse()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new SimpleAuthenticationService(mockJsRuntime.Object);

        // Act
        var result = await service.LoginAsync("admin", "wrongpassword");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task LoginAsync_WithEmptyCredentials_ReturnsFalse()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new SimpleAuthenticationService(mockJsRuntime.Object);

        // Act
        var result = await service.LoginAsync("", "");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task LoginAsync_WithNullPassword_ReturnsFalse()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new SimpleAuthenticationService(mockJsRuntime.Object);

        // Act
        var result = await service.LoginAsync("admin", null!);

        // Assert
        Assert.False(result);
    }
}
