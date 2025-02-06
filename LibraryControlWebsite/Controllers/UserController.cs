using LibaryControlWebsite.Models;
using LibaryControlWebsite.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using LibaryControlWebsite.Models.Responsibility;

namespace LibaryControlWebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Email and password are required.");
                return View("LoginFailed");
            }

            var user = await _userService.Login(email, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View("LoginFailed");
            }
            Console.WriteLine("Login successful: " + user.FullName);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user, string confirmPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var newUser = await _userService.Register(user, confirmPassword);
    
            if (newUser == null)
            {
                ModelState.AddModelError("", "Registration failed. Email may already exist or passwords do not match.");
                return View(user);
            }

            return RedirectToAction("Login");
        }
    }
}
