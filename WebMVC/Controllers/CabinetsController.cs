﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Data;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "user, admin")]
    public class CabinetsController : Controller
    {
        private readonly TechInventContext _context;
        private readonly ExcelService _excelService;

        public CabinetsController(TechInventContext context, ExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
        }

        // GET: Cabinets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cabinets.AsNoTracking().Include(c => c.Workplaces).ToListAsync());
        }

        public async Task<IActionResult> GenerateReport()
        {
            var cabinet = _context.Cabinets.AsNoTracking().Include(c => c.Workplaces).ThenInclude(w => w.IdOsNavigation);

            if (cabinet.Count() == 0)
            {
                return NotFound();
            }

            return File(await _excelService.GenerateCabinetsReportAsync(await cabinet.ToListAsync()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"CabinetsReport.xlsx");
        }
        public async Task<IActionResult> GenerateReportById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cabinet = _context.Cabinets.AsNoTracking().Include(c => c.Workplaces).ThenInclude(w => w.IdOsNavigation).FirstOrDefault(c => c.IdCabinet == id);

            if (cabinet == null)
            {
                return NotFound();
            }

            return File(await _excelService.GenerateCabinetReportAsync(cabinet), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{cabinet.Name}.xlsx");
        }
    }
}
