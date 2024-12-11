using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public WorkplacesController(TechInventContext context, ExcelService exceltService)
        {
            _context = context;
            _excelService = exceltService;
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
            var workplace = await _context.Workplaces
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
                        .ThenInclude(s => s.ManufacturerNavigation)
                .FirstOrDefaultAsync(m => m.IdWorkplace == id);
            if (workplace == null)
            {
                return NotFound();
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
            var workplace = await _context.Workplaces
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
                        .ThenInclude(s => s.ManufacturerNavigation)
                .FirstOrDefaultAsync(m => m.Guid == id);

            if (workplace == null)
            {
                return NotFound();
            }

            ViewData["PCName"] = workplace.Name;

            return View(workplace);
        }
        public async Task<IActionResult> SoftwareList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.workplaceId = id;
            var workplace = await _context.Workplaces
                .AsNoTracking()
                .Include(w => w.IdCabinetNavigation)
                .Include(w => w.InstalledSoftware)
                    .ThenInclude(s => s.SoftwareNavigation)
                        .ThenInclude(s => s.ManufacturerNavigation)
                .FirstOrDefaultAsync(m => m.IdWorkplace == id);
            if (workplace == null)
            {
                return NotFound();
            }

            ViewData["PCName"] = workplace.Name;
            var groups = workplace.InstalledSoftware.GroupBy(g => g.SoftwareNavigation.IdManufacturer);

            return View(groups);
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
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(Url.Action("Public", "Workplaces", new { id=workplace.Guid }, scheme, url), QRCodeGenerator.ECCLevel.Q)) //Url.Page("./Workplaces/Details/" + id)
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                qrCodeImage = qrCode.GetGraphic(5);
            }
            ViewData["qrBinary"] = Convert.ToBase64String(qrCodeImage);
            ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            return View();
        }
        public async Task<IActionResult> GenerateReport()
        {
            var workplaces = _context.Workplaces
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

            if (workplaces.Count() == 0)
            {
                return NotFound();
            }

            return File(await _excelService.GenerateWorkplacesReportAsync(await workplaces.ToListAsync()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"WorkplacesReport.xlsx");
        }
    }
}
