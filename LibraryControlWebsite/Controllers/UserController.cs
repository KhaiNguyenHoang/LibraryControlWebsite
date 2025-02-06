using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LibaryControlWebsite.Models.Responsibility;
using LibaryControlWebsite.Models;

namespace LibaryControlWebsite.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Hiển thị menu người dùng
        /// </summary>
        public async Task<IActionResult> UserMenu()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var user = await _userService.GetCurrentUser(userId.Value);
            return View(user);
        }

        /// <summary>
        /// Hiển thị form chỉnh sửa thông tin
        /// </summary>
        public async Task<IActionResult> Edit()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var user = await _userService.GetCurrentUser(userId.Value);
            return View(user);
        }

        /// <summary>
        /// Xử lý cập nhật thông tin người dùng
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(string FullName, string Phone, string Address)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var user = await _userService.GetCurrentUser(userId.Value);
            if (user == null) return RedirectToAction("Login", "Auth");

            user.FullName = FullName;
            user.Phone = Phone;
            user.Address = Address;

            bool updateSuccess = await _userService.Update(user);
            if (!updateSuccess) ViewBag.Error = "Cập nhật thất bại.";

            return RedirectToAction("UserMenu");
        }

        /// <summary>
        /// Hiển thị form đổi mật khẩu
        /// </summary>
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Xử lý đổi mật khẩu
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string newPassword)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var user = await _userService.GetCurrentUser(userId.Value);
            if (user == null) return RedirectToAction("UserMenu");

            await _userService.ChangePassword(user.UserId, newPassword);
            return RedirectToAction("UserMenu");
        }

        /// <summary>
        /// Hiển thị trang xác nhận xóa tài khoản
        /// </summary>
        public async Task<IActionResult> Delete()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var user = await _userService.GetCurrentUser(userId.Value);
            return View(user);
        }

        /// <summary>
        /// Xử lý xóa tài khoản
        /// </summary>
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            var user = await _userService.GetCurrentUser(userId.Value);
            if (user == null) return RedirectToAction("UserMenu");

            if (user.UserType.ToLower() == "admin")
            {
                ViewBag.Error = "Không thể xóa tài khoản Admin.";
                return View("Delete", user);
            }

            await _userService.Delete(user.UserId);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
