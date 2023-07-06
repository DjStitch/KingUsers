using KingsUsers.Data;
using KingsUsers.Exceptions;
using KingsUsers.Interfaces;
using KingsUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace KingsUsers.Services;

public class UserService : IUserService
{
    private readonly KingsDbContext _dbContext;

    public UserService(KingsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateUser(User user)
    {
        // Validate user input and perform necessary checks
        // Add the user to the database using _dbContext
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateUser(int id, User user)
    {
        // Validate user input and perform necessary checks
        // Retrieve the user from the database using _dbContext
        var existingUser = await _dbContext.Users.FindAsync(id);
        if (existingUser == null) throw new NotFoundException("User not found");

        // Update the user properties with the provided data
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Address = user.Address;
        existingUser.About = user.About;

        // Save the changes to the database using _dbContext
        await _dbContext.SaveChangesAsync();

        return existingUser;
    }

    public async Task<bool> RemoveUser(int id)
    {
        // Retrieve the user from the database using _dbContext
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null) throw new NotFoundException("User not found");

        // Remove the user from the database using _dbContext
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<int> GetUserCount()
    {
        // Retrieve the total user count from the database using _dbContext
        var count = await _dbContext.Users.CountAsync();
        return count;
    }

    public async Task<Dictionary<string, int>> GetUsersPerGroupCount()
    {
        // Retrieve the count of users per group from the database using _dbContext
        var counts = await _dbContext.UserGroups
            .GroupBy(g => g.Group.GroupName)
            .ToDictionaryAsync(g => g.Key, g => g.Count());

        return counts;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(PaginationParameters paginationParameters)
    {
        return await _dbContext.Users
            .OrderBy(u => u.UserId)
            .Skip(paginationParameters.Offset)
            .Take(paginationParameters.PageSize)
            .ToListAsync();
    }

    public async Task<User?> GetUserAsync(int userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

}