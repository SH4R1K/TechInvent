using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using QRCoder;
using WebMVC.Data;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "user, admin")]
    public class WorkplacesController : Controller
    {
        private readonly TechInventContext _context;
        private readonly ExcelService _excelService;
        private readonly IMemoryCache _cache;

        public WorkplacesController(TechInventContext context, ExcelService exceltService, IMemoryCache cache)
        {
            _context = context;
            _excelService = exceltService;
            _cache = cache;
        }
        private IQueryable<Workplace> GetWorkplacesQuery()
        {
            return _context.Workplaces
                .AsNoTracking()
                .Include(w => w.IdCabinetNavigation)
                .Include(w => w.IdOsNavigation)
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

        [HttpGet("{Controller}/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            _cache.TryGetValue(id, out Workplace? workplace);
            
            if (workplace == null)
            {
                workplace = await GetWorkplacesQuery()
                    .FirstOrDefaultAsync(m => m.IdWorkplace == id);

                if (workplace == null)
                {
                    return NotFound();
                }
                _cache.Set(workplace.IdWorkplace, workplace, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
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
                _cache.Set(workplace.Guid, workplace, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
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

            if (!workplaces.Any())
            {
                return NotFound();
            }

            return File(await _excelService.GenerateWorkplacesSoftwareReportAsync(workplaces), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{workplaces.First().IdCabinetNavigation.Name}SoftwareWorkplacesReport.xlsx");
        }
    }
}
