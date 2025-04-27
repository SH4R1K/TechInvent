using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;
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

        [HttpPost]
        public async Task<IActionResult> Create(Cabinet cabinet)
        {
            _context.Cabinets.Add(cabinet);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cabinet cabinet)
        {
            var oldCabinet = await _context.Cabinets.FindAsync(cabinet.IdCabinet);

            if (oldCabinet == null)
                return NotFound();

            oldCabinet.Name = cabinet.Name;

            _context.Cabinets.Update(oldCabinet);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cabinet = await _context.Cabinets.Include(c => c.Workplaces).FirstOrDefaultAsync(c => c.IdCabinet == id);

            if (cabinet == null)
                return NotFound();
            var undifinedCabinet = await _context.Cabinets.FirstOrDefaultAsync(c => c.Name == "Не определённый");
            if (undifinedCabinet == null)
            {
                undifinedCabinet = new Cabinet { Name = "Не определённый" };
                _context.Cabinets.Add(undifinedCabinet);
            }

            cabinet.Workplaces.ForEach(c => c.IdCabinetNavigation = undifinedCabinet);
            _context.Cabinets.Remove(cabinet);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
