﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize]
    public class WorkplacesController : Controller
    {
        private readonly TechInventContext _context;
        private readonly ExcelService _excelService;
        private readonly QRService _qrService;
        private readonly IToastifyService _notifyService;
        private readonly IMemoryCache _cache;

        public WorkplacesController(TechInventContext context, ExcelService excelService, IMemoryCache cache, IToastifyService notifyService, QRService qrService)
        {
            _context = context;
            _excelService = excelService;
            _cache = cache;
            _notifyService = notifyService;
            _qrService = qrService;
        }
        private IQueryable<Workplace> GetWorkplacesQuery()
        {
            return _context.Workplaces
                .AsNoTracking()
                .Include(w => w.IdCabinetNavigation)
                .Include(w => w.IdOsNavigation)
                .Include(w => w.CabinetEquipments)
                    .ThenInclude(c => c.CabinetEquipmentType)
                .Include(w => w.CabinetEquipments)
                    .ThenInclude(c => c.Vendor)
                .Include(w => w.Components)
                    .ThenInclude(c => c.Gpu)
                .Include(w => w.Components)
                    .ThenInclude(c => c.Processor)
                .Include(w => w.Components)
                    .ThenInclude(c => c.Mainboard)
                .Include(w => w.Components)
                    .ThenInclude(c => c.NetAdapter)
                        .ThenInclude(n => n.AdapterTypeNavigation)
                .Include(w => w.Components)
                    .ThenInclude(c => c.NetAdapter)
                        .ThenInclude(n => n.IdManufacturerNavigation)
                .Include(w => w.Components)
                    .ThenInclude(c => c.Ram)
                        .ThenInclude(r => r.IdManufacturerNavigation)
                .Include(w => w.Components)
                    .ThenInclude(c => c.Disk)
                .Include(w => w.InstalledSoftware)
                    .ThenInclude(s => s.SoftwareNavigation)
                        .ThenInclude(s => s.ManufacturerNavigation);
        }

        public async Task<IActionResult> Index(int id,  int page = 1)
        {
            int pageSize = 8;
            var techInventContext = _context.Workplaces.AsNoTracking()
                .Where(w => !w.IsDecommissioned).OrderByDescending(w => w.LastUpdate).Where(w => w.IdCabinet == id).Include(w => w.IdCabinetNavigation).Include(w => w.IdOsNavigation).AsQueryable();
            int totalItems = techInventContext.Count();
            techInventContext = techInventContext.Skip((page - 1) * pageSize).Take(pageSize);
            ViewData["Page"] = page;
            ViewData["PageCount"] = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.cabinetEquipments = await _context.CabinetEquipments.AsNoTracking().Include(eq => eq.CabinetEquipmentType).Include(eq => eq.Vendor).Where(eq => eq.IdCabinet == id).ToListAsync();
            ViewBag.cabinetName = _context.Cabinets.AsNoTracking().FirstOrDefault(c => c.IdCabinet == id)?.Name;
            ViewBag.cabinets = await _context.Cabinets.ToListAsync();
            return View(await techInventContext.ToListAsync());
        }

        [Authorize(Roles = "operator, admin")]
        [HttpPost]
        public async Task<IActionResult> DetachEquipment(int id, int idEquipment)
        {
            var cabinetEquipment = await _context.CabinetEquipments.FindAsync(idEquipment);

            if (cabinetEquipment == null)
                return NotFound();

            if (cabinetEquipment.IdWorkplace != id)
                return BadRequest("Оборудование не связано с этим рабочим местом");


            cabinetEquipment.IdWorkplace = null;

            _context.CabinetEquipments.Update(cabinetEquipment);
            _context.SaveChanges();

            _notifyService.Success("Оборудование откреплено");
            return RedirectToAction("Details", new { id });
        }

        [Authorize(Roles = "operator, admin")]
        [HttpPost]
        public async Task<IActionResult> MoveWorkplace(int id, int idCabinet)
        {
            var cabinet = await _context.Cabinets.FindAsync(idCabinet);

            if (cabinet == null)
                return NotFound();

            var workplace = await _context.Workplaces.FindAsync(id);

            if (workplace == null)
                return NotFound();

            workplace.IdCabinet = idCabinet;

            _context.Workplaces.Update(workplace);
            _context.SaveChanges();

            _notifyService.Success("Рабочее место перенесено");
            return RedirectToAction("Index", new { id = idCabinet });
        }

        [Authorize(Roles = "operator, admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateInventNumber(int id, string? inventNumber)
        {
            try
            {
                var workplace = await _context.Workplaces.FindAsync(id);

                if (workplace == null)
                    return NotFound();

                if (inventNumber != null)
                {
                    inventNumber = inventNumber?.Trim();
                }

                workplace.InventNumber = inventNumber;

                _context.Workplaces.Update(workplace);
                _context.SaveChanges();
                _notifyService.Success("Инвентарный номер обновлен");
                return RedirectToAction("Details", new { id });
            }
            catch
            {
                _notifyService.Error("Инвентарный номер уже занят");
                return RedirectToAction("Details", new { id });
            }
        }

        [Authorize(Roles = "operator, admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateSerialNumber(int id, string? serialNumber)
        {
            try
            {
                var workplace = await _context.Workplaces.FindAsync(id);

                if (workplace == null)
                    return NotFound();

                if (serialNumber != null)
                {
                    serialNumber = serialNumber?.Trim();
                }

                workplace.SerialNumber = serialNumber;

                _context.Workplaces.Update(workplace);
                _context.SaveChanges();
                _notifyService.Success("Серийный номер обновлен");
                return RedirectToAction("Details", new { id });
            }
            catch
            {
                _notifyService.Error("Серийный номер уже занят");
                return RedirectToAction("Details", new { id });
            }
        }

        [HttpGet("{Controller}/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _cache.TryGetValue(id, out Workplace? workplace);
            // Workplace? workplace = null;
            if (workplace == null)
            {
                workplace = await GetWorkplacesQuery()
                    .Include(w => w.AttachedTechRequests.Where(tr => tr.IsActive))
                    .FirstOrDefaultAsync(m => m.IdInventStuff == id);

                if (workplace == null)
                {
                    return NotFound();
                }
                //_cache.Set(workplace.IdWorkplace, workplace, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }

            ViewData["PCName"] = workplace.Name;

            return View(workplace);
        }

        [AllowAnonymous]
        [HttpGet("{Controller}/Public/{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _cache.TryGetValue(id, out Workplace? workplace);

            if (workplace == null)
            {
                workplace = await GetWorkplacesQuery()
                    .FirstOrDefaultAsync(m => m.Guid == id);

                if (workplace == null)
                {
                    return NotFound();
                }
                _cache.Set(workplace.Guid, workplace, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }

            ViewData["PCName"] = workplace.Name;

            return View(workplace);
        }

        public async Task<IActionResult> QRCode(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workplace = await _context.Workplaces.AsNoTracking().Where(w => !w.IsDecommissioned).FirstOrDefaultAsync(w => w.IdInventStuff == id);

            if (workplace == null)
            {
                return NotFound();
            }

            string qrCodeImage;
            string scheme = Request.Scheme;
            string url = Request.Host.ToString();

            qrCodeImage = _qrService.GenerateQrBase64(Url.Action("Public", "Workplaces", new { id = workplace.Guid }, scheme, url));

            ViewData["qrBinary"] = qrCodeImage;
            ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            return View();
        }

        [AllowAnonymous]
        [HttpGet("{Controller}/PublicReportHardware/{id}")]
        public async Task<IActionResult> PublicGenerateHardwareReportById(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _cache.TryGetValue(id, out Workplace? workplace);

            if (workplace == null)
            {
                workplace = await GetWorkplacesQuery()
                    .FirstOrDefaultAsync(m => m.Guid == id);

                if (workplace == null)
                {
                    return NotFound();
                }
                _cache.Set(workplace.Guid, workplace, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }

            return File(await _excelService.GenerateWorkplaceHardwareReportAsync(workplace), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{workplace.Name}Hardware.xlsx");
        }

        public async Task<IActionResult> GenerateHardwareReportByIdCabinet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workplaces = await GetWorkplacesQuery()
                .Where(w => !w.IsDecommissioned)
                .Where(w => w.IdCabinet == id)
                .ToListAsync();

            if (!workplaces.Any())
            {
                return NotFound();
            }

            return File(await _excelService.GenerateCabinetWorkplacesHardwareReportAsync(workplaces), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{workplaces.First().IdCabinetNavigation.Name}HardwareWorkplacesReport.xlsx");
        }

        [AllowAnonymous]
        [HttpGet("{Controller}/PublicReportSoftware/{id}")]
        public async Task<IActionResult> PublicGenerateSoftwareReportById(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            _cache.TryGetValue(id, out Workplace? workplace);

            if (workplace == null)
            {
                workplace = await GetWorkplacesQuery()
                    .FirstOrDefaultAsync(m => m.Guid == id);

                if (workplace == null)
                {
                    return NotFound();
                }
                _cache.Set(workplace.Guid, workplace, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
            }

            return File(await _excelService.GenerateWorkplaceSoftwareReportAsync(workplace), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{workplace.Name}Software.xlsx");
        }

        public async Task<IActionResult> GenerateSoftwareReportByIdCabinet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workplaces = await GetWorkplacesQuery()
                .Where(w => !w.IsDecommissioned)
                .Where(w => w.IdCabinet == id)
                .ToListAsync();

            var installedSoftwareIds = workplaces
                .SelectMany(w => w.InstalledSoftware.Select(ins => ins.IdSoftware))
                .Distinct()
                .ToList();

            var software = await _context.Softwares
                .Include(s => s.InstalledSoftware)
                .Where(s => installedSoftwareIds.Contains(s.IdSoftware))
                .ToListAsync();

            if (!workplaces.Any())
            {
                return NotFound();
            }

            return File(await _excelService.GenerateCabinetWorkplacesSoftwareReportAsync(workplaces, software), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{workplaces.First().IdCabinetNavigation.Name}SoftwareWorkplacesReport.xlsx");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workplace = await _context.Workplaces.FirstOrDefaultAsync(w => w.IdInventStuff == id);

            if (workplace == null)
            {
                return NotFound();
            }

            _context.Workplaces.Remove(workplace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = workplace.IdCabinet });
        }
    }
}
