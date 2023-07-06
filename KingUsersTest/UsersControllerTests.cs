using KingsUsers.Controllers;
using KingsUsers.Exceptions;
using KingsUsers.Interfaces;
using KingsUsers.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace KingsUsers.Tests
{
    public class UsersControllerTests
    {
        private UsersController CreateController(IUserService userService)
        {
            return new UsersController(userService);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var users = GetTestUsers();
            userServiceMock.Setup(x => x.GetAllUsersAsync(It.IsAny<PaginationParameters>()))
                           .ReturnsAsync(users);
            var controller = CreateController(userServiceMock.Object);

            // Act
            var result = await controller.SearchAllUsers(new PaginationParameters());

            // Assert
            var expectedUsers = GetTestUsers();
            Assert.IsType<List<User>>(result);
            Assert.Equal(expectedUsers, result);
        }

        [Fact]
        public async Task GetUser_WithValidUserId_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var userServiceMock = new Mock<IUserService>();
            var user = GetTestUsers().FirstOrDefault(u => u.UserId == userId);
            userServiceMock.Setup(x => x.GetUserAsync(userId))
                           .ReturnsAsync(user);
            var controller = CreateController(userServiceMock.Object);

            // Act
            var result = await controller.GetUser(userId);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task CreateUser_WithValidUser_CreatesUser()
        {
            // Arrange
            var user = new User { UserId = 6, Username = "user3", FirstName = "Mark", LastName = "Johnson", Address = "Australia", About = "New User" };
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetUserAsync(user.UserId))
                           .ReturnsAsync((User)null); // User does not exist
            userServiceMock.Setup(x => x.CreateUser(user))
                           .ReturnsAsync(user); // Return the created user
            var controller = CreateController(userServiceMock.Object);

            // Act
            var result = await controller.CreateUser(user);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task CreateUser_WithInvalidGroup_ThrowsBadRequestException()
        {
            // Arrange
            var user = new User { UserId = 7, Username = "user4", FirstName = "Sarah", LastName = "Smith", Address = "USA", About = "User without group" };
            var userServiceMock = new Mock<IUserService>();
            var controller = CreateController(userServiceMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => controller.CreateUser(user));
        }

        [Fact]
        public async Task UpdateUser_WithValidIdAndUser_ReturnsUpdatedUser()
        {
            // Arrange
            var userId = 1;
            var updatedUser = new User { UserId = 1, Username = "king1", FirstName = "Arthur", LastName = "Pendragon", Address = "Updated Address", About = "Updated About" };
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.UpdateUser(userId, updatedUser))
                           .ReturnsAsync(updatedUser);
            var controller = CreateController(userServiceMock.Object);

            // Act
            var result = await controller.UpdateUser(userId, updatedUser);

            // Assert
            Assert.Equal(updatedUser, result);
        }

        [Fact]
        public async Task RemoveUser_WithValidId_ReturnsTrue()
        {
            // Arrange
            var userId = 2;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.RemoveUser(userId))
                           .ReturnsAsync(true);
            var controller = CreateController(userServiceMock.Object);

            // Act
            var result = await controller.RemoveUser(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UserCount_ReturnsUserCount()
        {
            // Arrange
            var expectedCount = 5; // Total number of test users
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetUserCount())
                           .ReturnsAsync(expectedCount);
            var controller = CreateController(userServiceMock.Object);

            // Act
            var result = await controller.UserCount();

            // Assert
            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public async Task UsersPerGroupCount_ReturnsDictionaryWithUsersPerGroupCount()
        {
            // Arrange
            var expectedGroupCount = new Dictionary<string, int>
            {
                { "Group1", 2 }, // Number of users in Group1
                { "Group2", 1 }, // Number of users in Group2
                { "Group3", 1 }  // Number of users in Group3
            };
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetUsersPerGroupCount())
                           .ReturnsAsync(expectedGroupCount);
            var controller = CreateController(userServiceMock.Object);

            // Act
            var result = await controller.UsersPerGroupCount();

            // Assert
            Assert.Equal(expectedGroupCount, result);
        }

        private List<User> GetTestUsers()
        {
            return new List<User>
            {
                new User { UserId = 1, Username = "king1", FirstName = "Arthur", LastName = "Pendragon", Address = "Camelot", About = "Legendary King of the Britons" },
                new User { UserId = 2, Username = "king2", FirstName = "Richard", LastName = "the Lionheart", Address = "England", About = "King of England and Crusader" },
                new User { UserId = 3, Username = "king3", FirstName = "Charlemagne", LastName = "", Address = "Frankish Kingdom", About = "Founder of the Holy Roman Empire" },
                new User { UserId = 4, Username = "user1", FirstName = "John", LastName = "Doe", Address = "USA", About = "Regular User" },
                new User { UserId = 5, Username = "user2", FirstName = "Jane", LastName = "Smith", Address = "Canada", About = "Regular User" }
            };
        }
    }
}
