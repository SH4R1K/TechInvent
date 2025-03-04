using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "user, admin")]
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
            ViewData["IdRequestType"] = new SelectList(_context.RequestTypes, "IdRequestType", "Name");
            ViewData["IdCabinet"] = new SelectList(_context.Cabinets, "IdCabinet", "Name");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var techRequest = await _context.TechRequests.FindAsync(id);
            if (techRequest == null)
            {
                return NotFound();
            }

            _context.TechRequests.Remove(techRequest);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
