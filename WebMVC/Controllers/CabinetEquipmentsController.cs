using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace WebMVC.Controllers
{
    public class CabinetEquipmentsController : Controller
    {
        private readonly TechInventContext _context;

        public CabinetEquipmentsController(TechInventContext context)
        {
            _context = context;
        }

        // GET: CabinetEquipments
        public async Task<IActionResult> Index()
        {
            var techInventContext = _context.CabinetEquipments.Include(c => c.Cabinet).Include(c => c.CabinetEquipmentType);
            return View(await techInventContext.ToListAsync());
        }

        // GET: CabinetEquipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinetEquipment = await _context.CabinetEquipments
                .Include(c => c.Cabinet)
                .Include(c => c.CabinetEquipmentType)
                .FirstOrDefaultAsync(m => m.IdCabinetEquipment == id);
            if (cabinetEquipment == null)
            {
                return NotFound();
            }

            return View(cabinetEquipment);
        }

        // GET: CabinetEquipments/Create
        public IActionResult Create()
        {
            ViewData["IdCabinet"] = new SelectList(_context.Cabinets, "IdCabinet", "Name");
            ViewData["IdCabinetEquipmentType"] = new SelectList(_context.CabinetEquipmentTypes, "IdCabinetEquipmentType", "Name");
            return View();
        }

        // POST: CabinetEquipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCabinetEquipment,Name,InventNumber,IdCabinet,IdCabinetEquipmentType")] CabinetEquipment cabinetEquipment)
        {
            _context.Add(cabinetEquipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CabinetEquipments/Edit/5
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
            ViewData["IdCabinet"] = new SelectList(_context.Cabinets, "IdCabinet", "Name", cabinetEquipment.IdCabinet);
            ViewData["IdCabinetEquipmentType"] = new SelectList(_context.CabinetEquipmentTypes, "IdCabinetEquipmentType", "Name", cabinetEquipment.IdCabinetEquipmentType);
            return View(cabinetEquipment);
        }

        // POST: CabinetEquipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCabinetEquipment,Name,InventNumber,IdCabinet,IdCabinetEquipmentType")] CabinetEquipment cabinetEquipment)
        {
            if (id != cabinetEquipment.IdCabinetEquipment)
            {
                return NotFound();
            }

            try
            {
                _context.Update(cabinetEquipment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabinetEquipmentExists(cabinetEquipment.IdCabinetEquipment))
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

        // GET: CabinetEquipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabinetEquipment = await _context.CabinetEquipments
                .Include(c => c.Cabinet)
                .Include(c => c.CabinetEquipmentType)
                .FirstOrDefaultAsync(m => m.IdCabinetEquipment == id);
            if (cabinetEquipment == null)
            {
                return NotFound();
            }

            return View(cabinetEquipment);
        }

        // POST: CabinetEquipments/Delete/5
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
            return _context.CabinetEquipments.Any(e => e.IdCabinetEquipment == id);
        }
    }
}
