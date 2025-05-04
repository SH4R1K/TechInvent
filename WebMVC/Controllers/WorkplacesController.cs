using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QRCoder;
using System.Linq;
using System.Linq.Expressions;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "user, admin")]
    public class WorkplacesController : Controller
    {
        private readonly TechInventContext _context;
        private readonly ExcelService _excelService;
        private readonly IMemoryCache _cache;

        public WorkplacesController(TechInventContext context, ExcelService excelService, IMemoryCache cache)
        {
            _context = context;
            _excelService = excelService;
            _cache = cache;
        }
        private IQueryable<Workplace> GetWorkplacesQuery()
        {
            return _context.Workplaces
                .AsNoTracking()
                .Include(w => w.IdCabinetNavigation)
                .Include(w => w.IdOsNavigation)
                .Include(w => w.Monitors)
                .Include(w => w.Components)
                    .ThenInclude(c => c.Gpu)
                .Include(w => w.Components)
                    .ThenInclude(c => c.Processor)
                .Include(w => w.Components)
                    .ThenInclude(c => c.Mainboard)
                .Include(w => w.Components)
                    .ThenInclude(c => c.NetAdapter)
                        .ThenInclude(n => n.AdapterTypeIdAdapterTypeNavigation)
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

        // GET: Workplaces
        public async Task<IActionResult> Index(int? id)
        {
            var techInventContext = _context.Workplaces.AsNoTracking().OrderByDescending(w => w.LastUpdate).Where(w => w.IdCabinet == id).Include(w => w.IdCabinetNavigation).Include(w => w.IdOsNavigation);
            ViewBag.cabinetName = _context.Cabinets.AsNoTracking().FirstOrDefault(c => c.IdCabinet == id)?.Name;
            return View(await techInventContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> DetachMonitor(int id, int idMonitor)
        {
            var monitor = await _context.Monitors.FindAsync(idMonitor);

            if (monitor == null)
                return NotFound();

            if (monitor.IdWorkplace != id)
                return BadRequest("Монитор не связан с этим рабочим местом");


            monitor.IdWorkplace = null;

            _context.Monitors.Update(monitor);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInventNumber(int id, string? inventNumber)
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
            return RedirectToAction("Details", new { id });
        }
        public async Task<IActionResult> Search(string? query, bool searchByComponent = true, bool searchBySoftware = true)
        {

            ViewBag.query = query;
            Expression<Func<Workplace, bool>> predicate = PredicateBuilder.New<Workplace>(
                workplaces => workplaces.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                workplaces.InventNumber.Contains(query, StringComparison.OrdinalIgnoreCase));
            
            if (searchByComponent)
               predicate = predicate.Or(w => w.Components.Any(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));

            if (searchBySoftware)
                predicate = predicate.Or(w => w.InstalledSoftware.Any(s => s.SoftwareNavigation.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));

            var cabinets = _context.Cabinets
                .Include(w => w.Workplaces.AsQueryable().Where(predicate))
                    .ThenInclude(w => w.Components)
                .Include(w => w.Workplaces.AsQueryable().Where(predicate))
                    .ThenInclude(w => w.InstalledSoftware)
                        .ThenInclude(s => s.SoftwareNavigation)
                .AsQueryable();

            cabinets = cabinets.Where(c => c.Workplaces.AsQueryable().Any(predicate));
            

            return View(await cabinets.ToListAsync());
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
                    .FirstOrDefaultAsync(m => m.IdWorkplace == id);

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

            var workplace = await _context.Workplaces.AsNoTracking().FirstOrDefaultAsync(w => w.IdWorkplace == id);

            if (workplace == null)
            {
                return NotFound();
            }
            byte[] qrCodeImage = null;
            byte[] data = null;
            string scheme = Request.Scheme;
            string url = Request.Host.ToString();
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(Url.Action("Public", "Workplaces", new { id = workplace.Guid }, scheme, url), QRCodeGenerator.ECCLevel.Q)) //Url.Page("./Workplaces/Details/" + id)
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                qrCodeImage = qrCode.GetGraphic(5);
            }
            ViewData["qrBinary"] = Convert.ToBase64String(qrCodeImage);
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
                .Where(w => w.IdCabinet == id)
                .ToListAsync();
            var software = await _context.Softwares.ToListAsync();

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

            var workplace = await _context.Workplaces.FirstOrDefaultAsync(w => w.IdWorkplace == id);

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
