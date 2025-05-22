using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace WebMVC.Controllers
{
    [Authorize]
    public class InventStuffController : Controller
    {
        private readonly TechInventContext _context;

        public InventStuffController(TechInventContext context)
        {
            _context = context;
        }

        private IQueryable<InventStuff> GetIActualnventStuffQuery()
        {
            return _context.InventStuffs
                            .AsNoTracking()
                            .Where(ins => !ins.IsDecommissioned);
        }

        public async Task<IActionResult> DecommissionedStuff()
        {
            var inventStuff = new List<InventStuff>();

            var workplace = await _context.Workplaces
                .AsNoTracking()
                .Include(w => w.IdCabinetNavigation)
                .Include(w => w.IdOsNavigation)
                .Include(w => w.Components)
                .Include(w => w.InstalledSoftware)
                    .ThenInclude(s => s.SoftwareNavigation)
                        .ThenInclude(s => s.ManufacturerNavigation)
                .Where(ins => ins.IsDecommissioned).ToListAsync();

            inventStuff.AddRange(workplace);

            var cabinetEquipment = await _context.CabinetEquipments
                .AsNoTracking()
                .Include(w => w.Cabinet)
                .Include(w => w.CabinetEquipmentType)
                .Include(w => w.Workplace)
                .Include(w => w.Vendor)
                .Where(ins => ins.IsDecommissioned)
                .ToListAsync();

            inventStuff.AddRange(cabinetEquipment);
            return View(inventStuff);
        }

        public async Task<IActionResult> Search(string? query, bool searchByComponent = true, bool searchBySoftware = true)
        {
            ViewBag.query = query;
            var inventStuff = new List<InventStuff>();

            Expression<Func<Workplace, bool>> predicate = PredicateBuilder.New<Workplace>(
                workplaces => workplaces.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                workplaces.InventNumber.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                workplaces.SerialNumber.Contains(query, StringComparison.OrdinalIgnoreCase));

            if (searchByComponent)
                predicate = predicate.Or(w => w.Components.Any(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));

            if (searchBySoftware)
                predicate = predicate.Or(w => w.InstalledSoftware.Any(s => s.SoftwareNavigation.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));

            var workplace = _context.Workplaces
                .AsNoTracking()
                .Where(w => !w.IsDecommissioned)
                .Include(w => w.IdCabinetNavigation)
                .Include(w => w.IdOsNavigation)
                .Include(w => w.Components)
                .Include(w => w.InstalledSoftware)
                    .ThenInclude(s => s.SoftwareNavigation)
                        .ThenInclude(s => s.ManufacturerNavigation)
                .Where(predicate).ToList();

            inventStuff.AddRange(workplace);

            var cabinetEquipment = _context.CabinetEquipments
                .AsNoTracking()
                .Where(w => !w.IsDecommissioned)
                .Include(w => w.Cabinet)
                .Include(w => w.CabinetEquipmentType)
                .Include(w => w.Workplace)
                .Include(w => w.Vendor)
                .Where(w => w.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || w.InventNumber.Contains(query, StringComparison.OrdinalIgnoreCase)
                || w.SerialNumber.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                 w.Vendor.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            inventStuff.AddRange(cabinetEquipment);

            return View(inventStuff);
        }

        [HttpGet]
        public JsonResult AutoComplete(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Json(new List<object>());
            }

            var componentResults = _context.Components
                .AsNoTracking()
                .Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Name);

            var softwareResults = _context.Softwares
                .AsNoTracking()
                .Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Name);

            var inventNumberResults = GetIActualnventStuffQuery()
                .Where(ins => ins.InventNumber.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(ins => ins.InventNumber);

            var inventNameResults = GetIActualnventStuffQuery()
                .Where(ins => ins.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(ins => ins.Name);

            var serialNumberResults = GetIActualnventStuffQuery()
                .Where(ins => ins.SerialNumber.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(ins => ins.SerialNumber);

            var vendorNameResults = _context.Vendors
                .AsNoTracking()
                .Where(ins => ins.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(ins => ins.Name);

            var result = componentResults
                .Union(inventNumberResults)
                .Union(inventNameResults)
                .Union(serialNumberResults)
                .Union(softwareResults)
                .Union(vendorNameResults)
                .Distinct()
                .ToList()
                .Take(10);



            return Json(result);
        }
    }
}
