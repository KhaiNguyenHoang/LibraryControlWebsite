using LibaryControlWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    Task<User?> Login(string email, string password);
    Task<User?> Register(User user, string confirmPassword);
    Task<bool> Update(User user);
    Task<User?> Delete(int id);
    Task<IEnumerable<User>> GetUsers();
    Task<User?> GetUser(int? id);
    Task<User?> GetCurrentUser(int userId);
    Task<bool> UserExists(string email);
    Task<User?> ChangePassword(int userId, string newPassword);
    Task<string> HashPassword(string password);
    Task<bool> UserPhoneExists(string phone);
}