namespace LibaryControlWebsite.Models.Responsibility;

public interface IUserService
{
    Task<User?> Login(string email, string password);
    Task<User?> Register(User user, string confirmPassword);
    Task<bool> Update(User user);
    Task<User> Delete(int id);
    Task<IEnumerable<User?>> GetUsers();
    Task<User?> GetUser(int? id);
    bool UserExists(string username);
    Task<User?> ChangePassword(User user, string newPassword);
    Task<String> HashPassword(string password);
}