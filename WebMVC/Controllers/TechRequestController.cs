using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace WebMVC.Controllers
{
    public class TechRequestController : Controller
    {
        private readonly TechInventContext _context;

        public TechRequestController(TechInventContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TechRequests.AsNoTracking().ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Workplaces = _context.Workplaces.ToList();
            return View();
        }
        public ActionResult Details(int id)
        {
            var techRequest = _context.TechRequests
                .Include(tr => tr.AttachedWorkplaces)
                    .ThenInclude(aw => aw.Workplace)
                .FirstOrDefault(tr => tr.IdRequest == id);

            if (techRequest == null)
            {
                return NotFound();
            }

            return View(techRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TechRequest model, int[] selectedWorkplaces)
        {
            if (ModelState.IsValid)
            {
                model.CreationDate = DateTime.UtcNow;
                if (selectedWorkplaces != null)
                {
                    model.AttachedWorkplaces = selectedWorkplaces
                        .Select(id => new TechRequestWorkplace { IdWorkplace = id, IdTechRequest = model.IdRequest })
                        .ToList();
                }

                _context.TechRequests.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Workplaces = _context.Workplaces.ToList();
            return View(model);
        }
    }
}
