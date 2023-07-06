using KingsUsers.Controllers;
using KingsUsers.Exceptions;
using KingsUsers.Interfaces;
using KingsUsers.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KingUsersTest;

public class UsersControllerTests
{
    private readonly UsersController _controller;
    private readonly Mock<IUserService> _userServiceMock;

    public UsersControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _controller = new UsersController(_userServiceMock.Object);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsOkResultWithUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new() { UserId = 1, Username = "user1", FirstName = "John", LastName = "Doe" },
            new() { UserId = 2, Username = "user2", FirstName = "Jane", LastName = "Smith" }
        };
        _userServiceMock.Setup(service => service.GetAllUsersAsync(It.IsAny<PaginationParameters>()))
            .ReturnsAsync(users);

        // Act
        var result = await _controller.SearchAllUsers(new PaginationParameters());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
        Assert.Equal(users, model);
    }


    [Fact]
    public async Task GetUser_WithValidId_ReturnsOkResultWithUser()
    {
        // Arrange
        var userId = 1;
        var user = new User { UserId = userId, Username = "user1", FirstName = "John", LastName = "Doe" };
        _userServiceMock.Setup(service => service.GetUserAsync(userId))
            .ReturnsAsync(user);

        // Act
        var result = await _controller.GetUser(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<User>(okResult.Value);
        Assert.Equal(user, model);
    }

    [Fact]
    public async Task GetUser_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var userId = 1;
        _userServiceMock.Setup(service => service.GetUserAsync(userId))
            .ThrowsAsync(new NotFoundException("User not found"));

        // Act
        var result = await _controller.GetUser(userId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact]
    public async Task UpdateUser_WithValidIdAndUser_ReturnsOkResultWithUpdatedUser()
    {
        // Arrange
        var userId = 1;
        var user = new User { UserId = userId, Username = "user1", FirstName = "John", LastName = "Doe" };
        var updatedUser = new User
            { UserId = userId, Username = "user1-updated", FirstName = "John", LastName = "Doe" };
        _userServiceMock.Setup(service => service.UpdateUser(userId, user))
            .ReturnsAsync(updatedUser);

        // Act
        var result = await _controller.UpdateUser(userId, user);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<User>(okResult.Value);
        Assert.Equal(updatedUser, model);
    }


    [Fact]
    public async Task RemoveUser_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var userId = 1;
        _userServiceMock.Setup(service => service.RemoveUser(userId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.RemoveUser(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<bool>(okResult.Value);
        Assert.True(value);
    }


    [Fact]
    public async Task GetUserCount_ReturnsOkResultWithUserCount()
    {
        // Arrange
        var userCount = 10;
        _userServiceMock.Setup(service => service.GetUserCount())
            .ReturnsAsync(userCount);

        // Act
        var result = await _controller.UserCount();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<int>(okResult.Value);
        Assert.Equal(userCount, value);
    }

    [Fact]
    public async Task GetUsersPerGroupCount_ReturnsOkResultWithGroupCounts()
    {
        // Arrange
        var groupCounts = new Dictionary<string, int>
        {
            { "Kings", 3 },
            { "Normal Users", 5 }
        };
        _userServiceMock.Setup(service => service.GetUsersPerGroupCount())
            .ReturnsAsync(groupCounts);

        // Act
        var result = await _controller.UsersPerGroupCount();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<Dictionary<string, int>>(okResult.Value);
        Assert.Equal(groupCounts, model);
    }
}