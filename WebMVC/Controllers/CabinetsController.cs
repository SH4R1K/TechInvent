using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "user, admin")]
    public class CabinetsController : Controller
    {
        private readonly TechInventContext _context;

        public CabinetsController(TechInventContext context)
        {
            _context = context;
        }

        // GET: Cabinets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cabinets.AsNoTracking().Include(c => c.Workplaces).ToListAsync());
        }
    }
}
