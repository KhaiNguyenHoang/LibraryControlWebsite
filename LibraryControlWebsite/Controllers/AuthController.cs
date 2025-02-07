using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using LibaryControlWebsite.Models.Responsibility;
using LibaryControlWebsite.Models;

namespace LibaryControlWebsite.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Hiển thị trang đăng nhập
        /// </summary>
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("UserMenu", "User");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var user = await _userService.Login(email, password);
                if (user == null)
                {
                    ViewBag.Error = "Sai tài khoản hoặc mật khẩu.";
                    return View();
                }

                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("UserMenu", "User");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        /// <summary>
        /// Hiển thị trang đăng ký tài khoản
        /// </summary>
        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("UserMenu", "User");
            }
            return View();
        }

        /// <summary>
        /// Xử lý đăng ký tài khoản
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register(User user, string confirmPassword)
        {
            if (await _userService.UserExists(user.Email))
            {
                ViewBag.Error = "Email đã tồn tại.";
                return View(user);
            }
            
            if (await _userService.UserPhoneExists(user.Phone))
            {
                ViewBag.Error = "Số điện thoại đã được sử dụng.";
                return View(user);
            }

            if (user.PasswordHash != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp.";
                return View(user);
            }

            var registeredUser = await _userService.Register(user, confirmPassword);
            if (registeredUser == null)
            {
                ViewBag.Error = "Đăng ký thất bại! Kiểm tra thông tin đăng ký.";
                return View(user);
            }

            HttpContext.Session.SetInt32("UserId", registeredUser.UserId);
            return RedirectToAction("UserMenu", "User");
        }

        /// <summary>
        /// Đăng xuất người dùng
        /// </summary>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
