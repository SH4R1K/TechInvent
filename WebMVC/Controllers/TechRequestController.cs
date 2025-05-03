using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "user, admin")]
    public class TechRequestController : Controller
    {
        private readonly TechInventContext _context;
        private readonly UserService _userService;

        public TechRequestController(TechInventContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TechRequests.Include(t => t.RequestType).AsNoTracking().ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Workplaces = await _context.Workplaces.Include(w => w.IdCabinetNavigation).ToListAsync();
            ViewData["IdRequestType"] = new SelectList(_context.RequestTypes, "IdRequestType", "Name");
            return View();
        }
        public ActionResult Details(int id)
        {
            var techRequest = _context.TechRequests
                .Include(tr => tr.AttachedWorkplaces)
                    .ThenInclude(aw => aw.Workplace)
                    .ThenInclude(w => w.IdCabinetNavigation)
                .Include(tr => tr.Comments)
                    .ThenInclude(c => c.Author)
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

        public async Task<IActionResult> CompleteWorkplace(int id, int idWorkplace)
        {
            var techRequest = await _context.TechRequests.Include(tr => tr.AttachedWorkplaces).FirstOrDefaultAsync(tr => tr.IdRequest == id);
            if (techRequest == null)
            {
                return NotFound();
            }

            var workplace = techRequest.AttachedWorkplaces.FirstOrDefault(a => a.IdWorkplace == idWorkplace);

            if (workplace == null)
            {
                return NotFound();
            }

            workplace.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id });
        }
        public async Task<IActionResult> RestoreWorkplace(int id, int idWorkplace)
        {
            var techRequest = await _context.TechRequests.Include(tr => tr.AttachedWorkplaces).FirstOrDefaultAsync(tr => tr.IdRequest == id);
            if (techRequest == null)
            {
                return NotFound();
            }

            var workplace = techRequest.AttachedWorkplaces.FirstOrDefault(a => a.IdWorkplace == idWorkplace);

            if (workplace == null)
            {
                return NotFound();
            }

            workplace.IsActive = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id });
        }
        public async Task<IActionResult> CloseRequest(int id)
        {
            var techRequest = await _context.TechRequests.Include(tr => tr.AttachedWorkplaces).FirstOrDefaultAsync(tr => tr.IdRequest == id);
            if (techRequest == null)
            {
                return NotFound();
            }
            
            techRequest.AttachedWorkplaces.ForEach(aw => aw.IsActive = false);

            techRequest.IsActive = false;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ReopenRequest(int id)
        {
            var techRequest = await _context.TechRequests.Include(tr => tr.AttachedWorkplaces).FirstOrDefaultAsync(tr => tr.IdRequest == id);
            if (techRequest == null)
            {
                return NotFound();
            }

            techRequest.IsActive = true;

            await _context.SaveChangesAsync();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentRequest(int id, string message)
        {
            var techRequest = await _context.TechRequests.FindAsync(id);
            if (techRequest == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(_userService.GetUserId());

            if (user == null)
                return Unauthorized();

            TechRequestComment comment = new TechRequestComment { Author = user, TechRequest = techRequest, Message = message, CommentDate = DateTime.UtcNow };
            techRequest.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id });
        }
    }
}
