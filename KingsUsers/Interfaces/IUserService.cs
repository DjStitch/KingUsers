using KingsUsers.Models;

namespace KingsUsers.Interfaces;

public interface IUserService
{
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(int id, User user);
    Task<bool> RemoveUser(int id);
    Task<int> GetUserCount();
    Task<Dictionary<string, int>> GetUsersPerGroupCount();
    Task<IEnumerable<User>> GetAllUsersAsync(PaginationParameters paginationParameters);
    Task<User?> GetUserAsync(int userId);
}