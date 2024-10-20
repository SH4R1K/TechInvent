using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechInventAPI.Data;

namespace WebMVC.Controllers
{
    public class WorkplacesController : Controller
    {
        private readonly TechInventContext _context;

        public WorkplacesController(TechInventContext context)
        {
            _context = context;
        }

        // GET: Workplaces
        public async Task<IActionResult> Index(int? id)
        {
            var techInventContext = _context.Workplaces.Where(w => w.IdCabinet == id).Include(w => w.IdCabinetNavigation).Include(w => w.IdOsNavigation);
            ViewBag.cabinetName = _context.Cabinets.FirstOrDefault(c => c.IdCabinet == id)?.Name;
            return View(await techInventContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workplace = await _context.Workplaces
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
                .FirstOrDefaultAsync(m => m.IdWorkplace == id);
            if (workplace == null)
            {
                return NotFound();
            }

            return View(workplace);
        }
    }
}
