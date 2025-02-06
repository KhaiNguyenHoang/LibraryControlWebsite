using LibaryControlWebsite.Models.Responsibility;
using Microsoft.EntityFrameworkCore;

namespace LibaryControlWebsite.Models.Service;

public class UserService : IUserService
{
    private readonly LibraryContext _context;

    public UserService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<User?> Login(string email, string password)
    {
        // Tìm người dùng theo email
        var userToLogin = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    
        if (userToLogin == null)
        {
            Console.WriteLine("Login failed: Email not found.");
            return null;
        }

        // Kiểm tra mật khẩu
        if (!BCrypt.Net.BCrypt.Verify(password, userToLogin.PasswordHash))
        {
            Console.WriteLine("Login failed: Incorrect password.");
            return null;
        }

        Console.WriteLine($"Login successful: {userToLogin.FullName}");
        return userToLogin;
    }


    public async Task<User?> Register(User user, string confirmPassword)
    {
        // Kiểm tra xem email đã tồn tại chưa
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            Console.WriteLine("Registration failed: Email already exists.");
            return null;
        }

        // Kiểm tra xác nhận mật khẩu
        if (user.PasswordHash != confirmPassword)
        {
            Console.WriteLine("Registration failed: Passwords do not match.");
            return null;
        }

        // Gán RoleId dựa vào UserType
        user.RoleId = user.UserType.ToLower() switch
        {
            "reader" => 1,
            "staff" => 2,
            "admin" => 3,
            _ => throw new ArgumentException("Invalid User Type")
        };

        // Hash password trước khi lưu vào DB
        user.PasswordHash = await HashPassword(user.PasswordHash);
        user.CreatedAt = DateTime.UtcNow;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        Console.WriteLine($"User {user.FullName} registered successfully!");
        return user;
    }


    public async Task<bool> Update(User user)
    {
        var userToUpdate = GetUsers().Result.FirstOrDefault(u => u.UserId ==user.UserId);
        if (userToUpdate != null)
        {
            userToUpdate.FullName = user.FullName;
            userToUpdate.Email = user.Email;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Address = user.Address;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<User?> Delete(int id)
    {
        var user = await GetUser(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
        return null;
    }

    public async Task<IEnumerable<User?>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUser(int? id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
    }

    public bool UserExists(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == username);
        return user != null;
    }

    public async Task<User?> ChangePassword(User user, string newPassword)
    {
        var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
        if (userToUpdate != null)
        {
            userToUpdate.PasswordHash = await HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return userToUpdate;
        }
        return null;
    }

    public async Task<string> HashPassword(string password)
    {
        return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
    }
}