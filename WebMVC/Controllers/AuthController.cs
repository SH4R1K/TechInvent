﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using WebMVC.Data;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly TechInventContext _context;
        private readonly JWTokenService _tokenService;
        public AuthController(TechInventContext context, JWTokenService tokenService) 
        { 
            _context = context;
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Login,Password")] User user)
        {
            var existUser = _context.User.Include(u => u.Role).FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
            if (existUser != null)
            {
                var token = _tokenService.CreateToken(existUser);
                Response.Cookies.Append("A", token);
                return RedirectToAction("Index", "Home");

            }
            return Unauthorized();
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("A");
            return RedirectToAction("Index", "Auth");
        }
    }
}
