using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace WebMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class CabinetEquipmentTypesController : Controller
    {
        private readonly TechInventContext _context;

        public CabinetEquipmentTypesController(TechInventContext context)
        {
            _context = context;
        }

        // GET: CabinetEquipmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CabinetEquipmentTypes.ToListAsync());
        }

        // GET: CabinetEquipmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinetEquipmentType = await _context.CabinetEquipmentTypes
                .FirstOrDefaultAsync(m => m.IdCabinetEquipmentType == id);
            if (cabinetEquipmentType == null)
            {
                return NotFound();
            }

            return View(cabinetEquipmentType);
        }

        // GET: CabinetEquipmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CabinetEquipmentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCabinetEquipmentType,Name")] CabinetEquipmentType cabinetEquipmentType)
        {
            _context.Add(cabinetEquipmentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CabinetEquipmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinetEquipmentType = await _context.CabinetEquipmentTypes.FindAsync(id);
            if (cabinetEquipmentType == null)
            {
                return NotFound();
            }
            return View(cabinetEquipmentType);
        }

        // POST: CabinetEquipmentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCabinetEquipmentType,Name")] CabinetEquipmentType cabinetEquipmentType)
        {
            if (id != cabinetEquipmentType.IdCabinetEquipmentType)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cabinetEquipmentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CabinetEquipmentTypeExists(cabinetEquipmentType.IdCabinetEquipmentType))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cabinetEquipmentType);
        }

        // GET: CabinetEquipmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinetEquipmentType = await _context.CabinetEquipmentTypes
                .FirstOrDefaultAsync(m => m.IdCabinetEquipmentType == id);
            if (cabinetEquipmentType == null)
            {
                return NotFound();
            }

            return View(cabinetEquipmentType);
        }

        // POST: CabinetEquipmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cabinetEquipmentType = await _context.CabinetEquipmentTypes.FindAsync(id);
            if (cabinetEquipmentType != null)
            {
                _context.CabinetEquipmentTypes.Remove(cabinetEquipmentType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CabinetEquipmentTypeExists(int id)
        {
            return _context.CabinetEquipmentTypes.Any(e => e.IdCabinetEquipmentType == id);
        }
    }
}
