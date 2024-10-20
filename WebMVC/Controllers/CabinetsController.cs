using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechInventAPI.Data;
using TechInventAPI.Models;

namespace WebMVC.Controllers
{
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
            return View(await _context.Cabinets.Include(c => c.Workplaces).ToListAsync());
        }
    }
}
