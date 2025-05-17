using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class InventStuffController : Controller
    {
        private readonly TechInventContext _context;

        public InventStuffController(TechInventContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Search(string? query, bool searchByComponent = true, bool searchBySoftware = true)
        {
            ViewBag.query = query;
            var inventStuff = new List<InventStuff>();

            Expression<Func<Workplace, bool>> predicate = PredicateBuilder.New<Workplace>(
                workplaces => workplaces.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                workplaces.InventNumber.Contains(query, StringComparison.OrdinalIgnoreCase));

            if (searchByComponent)
                predicate = predicate.Or(w => w.Components.Any(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));

            if (searchBySoftware)
                predicate = predicate.Or(w => w.InstalledSoftware.Any(s => s.SoftwareNavigation.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));

            var workplace = _context.Workplaces
                .AsNoTracking()
                .Include(w => w.IdCabinetNavigation)
                .Include(w => w.IdOsNavigation)
                .Include(w => w.Monitors)
                .Include(w => w.Components)
                .Include(w => w.InstalledSoftware)
                    .ThenInclude(s => s.SoftwareNavigation)
                        .ThenInclude(s => s.ManufacturerNavigation)
                .Where(predicate).ToList();

            inventStuff.AddRange(workplace);

            var monitor = _context.Monitors
                .AsNoTracking()
                .Where(w => w.Name.Contains(query, StringComparison.OrdinalIgnoreCase) 
                || w.InventNumber.Contains(query, StringComparison.OrdinalIgnoreCase)
                || w.SerialNumber.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Include(w => w.Workplace).ToList();

            inventStuff.AddRange(monitor);

            var cabinetEquipment = _context.CabinetEquipments
                .AsNoTracking()
                .Include(w => w.Cabinet)
                .Include(w => w.CabinetEquipmentType)
                .Where(w => w.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || w.InventNumber.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            inventStuff.AddRange(cabinetEquipment);

            return View(inventStuff);
        }
    }
}
