using LibaryControlWebsite.Models.Responsibility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibaryControlWebsite.Models.Service
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;

        public UserService(LibraryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Xử lý đăng nhập
        /// </summary>
        public async Task<User?> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Email hoặc mật khẩu không được để trống.");
            }

            email = email.ToLower(); // ✅ Chuyển email về chữ thường

            var userToLogin = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
            if (userToLogin == null) throw new Exception("Tài khoản không tồn tại.");
            if (!BCrypt.Net.BCrypt.Verify(password, userToLogin.PasswordHash)) throw new Exception("Mật khẩu không chính xác.");

            return userToLogin;
        }

        /// <summary>
        /// Xử lý đăng ký tài khoản
        /// </summary>
        public async Task<User?> Register(User user, string confirmPassword)
        {
            user.Email = user.Email.ToLower(); // ✅ Chuyển email về chữ thường

            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new Exception("Email đã được sử dụng.");

            if (user.PasswordHash != confirmPassword)
                throw new Exception("Mật khẩu xác nhận không khớp.");

            string userType = user.UserType.ToLower();
            if (userType != "reader" && userType != "staff" && userType != "admin")
                throw new Exception("Loại tài khoản không hợp lệ.");

            user.RoleId = userType switch
            {
                "reader" => 1,
                "staff" => 2,
                "admin" => 3,
                _ => throw new ArgumentException("Loại tài khoản không hợp lệ.")
            };

            user.PasswordHash = await HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng
        /// </summary>
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Lấy thông tin người dùng theo ID
        /// </summary>
        public async Task<User?> GetUser(int? id)
        {
            if (id == null) return null;
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        /// <summary>
        /// Lấy thông tin người dùng hiện tại
        /// </summary>
        public async Task<User?> GetCurrentUser(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại chưa
        /// </summary>
        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        public async Task<bool> Update(User user)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (userToUpdate == null) return false;

            userToUpdate.FullName = user.FullName ?? userToUpdate.FullName;
            userToUpdate.Phone = user.Phone ?? userToUpdate.Phone;
            userToUpdate.Address = user.Address ?? userToUpdate.Address;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        public async Task<User?> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null) return null;

            if (user.UserType.ToLower() == "admin") throw new Exception("Không thể xóa tài khoản Admin.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        public async Task<User?> ChangePassword(int userId, string newPassword)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (userToUpdate == null) return null;

            userToUpdate.PasswordHash = await HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return userToUpdate;
        }

        /// <summary>
        /// Hash mật khẩu bằng BCrypt
        /// </summary>
        public async Task<string> HashPassword(string password)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
        }
        
        public async Task<bool> UserPhoneExists(string phone)
        {
            return await _context.Users.AnyAsync(u => u.Phone == phone);
        }
    }
}
