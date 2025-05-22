using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechInvent.BLL.DtoModels.DtoMVC.Cabinet;
using TechInvent.BLL.DtoModels.DtoMVC.Vendor;
using TechInvent.BLL.DtoModels.DtoMVC.Workplace;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class CabinetEquipmentsController : Controller
    {
        private readonly TechInventContext _context;
        private readonly IToastifyService _notifyService;

        public CabinetEquipmentsController(TechInventContext context, IToastifyService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {
            var techInventContext = _context.CabinetEquipments.Include(c => c.Cabinet).Include(c => c.Workplace).Include(c => c.Vendor).Include(c => c.CabinetEquipmentType);
            return View(await techInventContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinetEquipment = await _context.CabinetEquipments
                .Include(c => c.Cabinet)
                .Include(c => c.CabinetEquipmentType)
                .FirstOrDefaultAsync(m => m.IdInventStuff == id);

            if (cabinetEquipment == null)
            {
                return NotFound();
            }

            return View(cabinetEquipment);
        }

        public IActionResult Create()
        {
            ViewData["IdCabinet"] = new SelectList(_context.Cabinets.Select(a => new CabinetNameIdDto { IdCabinet = a.IdCabinet, Name = a.Name }).ToList()
                .Prepend(new CabinetNameIdDto { Name = "Не выбран" }), "IdCabinet", "Name");
            ViewData["IdWorkplace"] = new SelectList(_context.Workplaces
                .Where(ins => !ins.IsDecommissioned).Select(a => new WorkplaceNameIdDto { IdWorkplace = a.IdCabinet, Name = a.Name }).ToList()
                .Prepend(new WorkplaceNameIdDto { Name = "Не выбран" }), "IdWorkplace", "Name");
            ViewData["IdVendor"] = new SelectList(_context.Vendors.Select(a => new VendorNameIdDto { IdVendor = a.IdVendor, Name = a.Name }).ToList()
                .Prepend(new VendorNameIdDto { Name = "Не выбран" }), "IdVendor", "Name");
            ViewData["IdCabinetEquipmentType"] = new SelectList(_context.CabinetEquipmentTypes, "IdCabinetEquipmentType", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInventStuff,Name,InventNumber,SerialNumber,IdWorkplace,IdCabinet,IdCabinetEquipmentType,IdVendor")] CabinetEquipment cabinetEquipment)
        {
            try
            {
                _context.Add(cabinetEquipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["IdCabinet"] = new SelectList(_context.Cabinets.Select(a => new CabinetNameIdDto { IdCabinet = a.IdCabinet, Name = a.Name }).ToList()
                    .Prepend(new CabinetNameIdDto { Name = "Не выбран" }), "IdCabinet", "Name");
                ViewData["IdWorkplace"] = new SelectList(_context.Workplaces
                    .Where(ins => !ins.IsDecommissioned).Select(a => new WorkplaceNameIdDto { IdWorkplace = a.IdCabinet, Name = a.Name }).ToList()
                    .Prepend(new WorkplaceNameIdDto { Name = "Не выбран" }), "IdWorkplace", "Name");
                ViewData["IdVendor"] = new SelectList(_context.Vendors.Select(a => new VendorNameIdDto { IdVendor = a.IdVendor, Name = a.Name }).ToList()
                    .Prepend(new VendorNameIdDto { Name = "Не выбран" }), "IdVendor", "Name");
                ViewData["IdCabinetEquipmentType"] = new SelectList(_context.CabinetEquipmentTypes, "IdCabinetEquipmentType", "Name");

                if (await _context.InventStuffs.AnyAsync(e => e.InventNumber == cabinetEquipment.InventNumber && cabinetEquipment.InventNumber != null))
                {
                    _notifyService.Error("Инвентарный номер уже занят");
                }

                if (await _context.InventStuffs.AnyAsync(e => e.SerialNumber == cabinetEquipment.SerialNumber && cabinetEquipment.SerialNumber != null))
                {
                    _notifyService.Error("Серийный номер уже занят");
                }

                return View(cabinetEquipment);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinetEquipment = await _context.CabinetEquipments.FindAsync(id);
            if (cabinetEquipment == null)
            {
                return NotFound();
            }

            ViewData["IdCabinet"] = new SelectList(_context.Cabinets.Select(a => new CabinetNameIdDto { IdCabinet = a.IdCabinet, Name = a.Name }).ToList()
                .Prepend(new CabinetNameIdDto { Name = "Не выбран" }), "IdCabinet", "Name", cabinetEquipment.IdCabinet);

            ViewData["IdWorkplace"] = new SelectList(_context.Workplaces
                .Where(ins => !ins.IsDecommissioned).Select(a => new WorkplaceNameIdDto { IdWorkplace = a.IdInventStuff, Name = a.Name }).ToList()
                .Prepend(new WorkplaceNameIdDto { Name = "Не выбран" }), "IdWorkplace", "Name", cabinetEquipment.IdWorkplace);
            ViewData["IdVendor"] = new SelectList(_context.Vendors.Select(a => new VendorNameIdDto { IdVendor = a.IdVendor, Name = a.Name }).ToList()
                .Prepend(new VendorNameIdDto { Name = "Не выбран" }), "IdVendor", "Name");

            ViewData["IdCabinetEquipmentType"] = new SelectList(_context.CabinetEquipmentTypes, "IdCabinetEquipmentType", "Name", cabinetEquipment.IdCabinetEquipmentType);

            return View(cabinetEquipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInventStuff,Name,InventNumber,SerialNumber,IdCabinet,IdWorkplace,IdCabinetEquipmentType,IdVendor")] CabinetEquipment cabinetEquipment)
        {
            if (id != cabinetEquipment.IdInventStuff)
            {
                return NotFound();
            }

            try
            {
                _context.Update(cabinetEquipment);
                await _context.SaveChangesAsync();
            }
            catch
            {
                ViewData["IdCabinet"] = new SelectList(_context.Cabinets.Select(a => new CabinetNameIdDto { IdCabinet = a.IdCabinet, Name = a.Name }).ToList()
                    .Prepend(new CabinetNameIdDto { Name = "Не выбран" }), "IdCabinet", "Name");
                ViewData["IdWorkplace"] = new SelectList(_context.Workplaces
                    .Where(ins => !ins.IsDecommissioned).Select(a => new WorkplaceNameIdDto { IdWorkplace = a.IdCabinet, Name = a.Name }).ToList()
                    .Prepend(new WorkplaceNameIdDto { Name = "Не выбран" }), "IdWorkplace", "Name");
                ViewData["IdVendor"] = new SelectList(_context.Vendors.Select(a => new VendorNameIdDto { IdVendor = a.IdVendor, Name = a.Name }).ToList()
                    .Prepend(new VendorNameIdDto { Name = "Не выбран" }), "IdVendor", "Name");
                ViewData["IdCabinetEquipmentType"] = new SelectList(_context.CabinetEquipmentTypes, "IdCabinetEquipmentType", "Name");

                if (await _context.InventStuffs.AnyAsync(e => e.IdInventStuff != cabinetEquipment.IdInventStuff && e.InventNumber == cabinetEquipment.InventNumber && cabinetEquipment.InventNumber != null))
                {
                    _notifyService.Error("Инвентарный номер уже занят");
                }

                if (await _context.InventStuffs.AnyAsync(e => e.IdInventStuff != cabinetEquipment.IdInventStuff && e.SerialNumber == cabinetEquipment.SerialNumber && cabinetEquipment.SerialNumber != null))
                {
                    _notifyService.Error("Серийный номер уже занят");
                }

                return View(cabinetEquipment);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinetEquipment = await _context.CabinetEquipments
                .Include(c => c.Cabinet)
                .Include(c => c.CabinetEquipmentType)
                .FirstOrDefaultAsync(m => m.IdInventStuff == id);
            if (cabinetEquipment == null)
            {
                return NotFound();
            }

            return View(cabinetEquipment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cabinetEquipment = await _context.CabinetEquipments.FindAsync(id);
            if (cabinetEquipment != null)
            {
                _context.CabinetEquipments.Remove(cabinetEquipment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CabinetEquipmentExists(int id)
        {
            return _context.CabinetEquipments.Any(e => e.IdInventStuff == id);
        }
    }
}
