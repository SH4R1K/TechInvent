using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly TechInventContext _context;
        private readonly JWTokenService _tokenService;
        private readonly IToastifyService _notifyService;
        public AuthController(TechInventContext context, JWTokenService tokenService, IToastifyService notifyService)
        {
            _context = context;
            _tokenService = tokenService;
            _notifyService = notifyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Login,Password")] User user)
        {
            try
            {
                var existUser = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
                if (existUser != null)
                {
                    var token = _tokenService.CreateToken(existUser);
                    Response.Cookies.Append("A", token);
                    return RedirectToAction("Index", "Home");

                }
                _notifyService.Error("Неверный логин или пароль");
                return Unauthorized();
            }
            catch
            {
                _notifyService.Error("Нет подключения к БД");
                return Unauthorized();  
            }
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("A");
            return RedirectToAction("Index", "Auth");
        }
    }
}
