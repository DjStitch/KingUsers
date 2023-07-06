using KingsUsers.Exceptions;
using KingsUsers.Interfaces;
using KingsUsers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KingsUsers.Controllers;



public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetAllUsers")]
    public async Task<IEnumerable<User>> SearchAllUsers([FromQuery] PaginationParameters paginationParameters)
    {
        return await _userService.GetAllUsersAsync(paginationParameters);
    }

    [HttpGet("{userId}")]
    public async Task<User?> GetUser(int userId)
    {
        return await _userService.GetUserAsync(userId);
    }

    [HttpPost]
    public async Task<User> CreateUser(User user)
    {
        if (user.UserGroups.IsNullOrEmpty() || user.UserGroups.Count == 0)
            throw new BadRequestException("Group need to be specified");

        var gotUser = await _userService.GetUserAsync(user.UserId);

        if (gotUser != null)
            throw new BadRequestException("User already exist");

        return await _userService.CreateUser(user);
    }

    [HttpPut("{id}")]
    public async Task<User> UpdateUser(int id, User user)
    {
        return await _userService.UpdateUser(id, user);
    }

    [HttpDelete("{id}")]
    public async Task<bool> RemoveUser(int id)
    {
        return await _userService.RemoveUser(id);
    }

    [HttpGet("count")]
    public async Task<int> UserCount()
    {
        return await _userService.GetUserCount();
    }

    [HttpGet("groupcount")]
    public async Task<Dictionary<string, int>> UsersPerGroupCount()
    {
        return await _userService.GetUsersPerGroupCount();
    }
}