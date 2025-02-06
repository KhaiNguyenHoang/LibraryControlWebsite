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

    public async Task<User?> Login(int userId, string password)
    {
        var userToLogin = await GetUser(userId);
        if (userToLogin != null && BCrypt.Net.BCrypt.Verify(password, userToLogin.PasswordHash))
        {
            return userToLogin;
        }
        return null;
    }

    public async Task<User> Register(User user)
    {
        user.PasswordHash = await HashPassword(user.PasswordHash);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
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