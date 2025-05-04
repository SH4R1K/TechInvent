using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;
using Monitor = TechInvent.DM.Models.Monitor;

namespace WebMVC.Controllers
{
    public class MonitorsController : Controller
    {
        private readonly TechInventContext _context;

        public MonitorsController(TechInventContext context)
        {
            _context = context;
        }

        // GET: Monitors
        public async Task<IActionResult> Index()
        {
            var techInventContext = _context.Monitors.Include(m => m.Workplace);
            return View(await techInventContext.ToListAsync());
        }

        // GET: Monitors/Create
        public IActionResult Create()
        {
            ViewData["IdWorkplace"] = new SelectList(_context.Workplaces, "IdWorkplace", "Name");
            return View();
        }

        // POST: Monitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMonitor,Name,InventNumber,IdWorkplace")] Monitor monitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdWorkplace"] = new SelectList(_context.Workplaces, "IdWorkplace", "Name", monitor.IdWorkplace);
            return View(monitor);
        }

        // GET: Monitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitor = await _context.Monitors.FindAsync(id);
            if (monitor == null)
            {
                return NotFound();
            }
            ViewData["IdWorkplace"] = new SelectList(_context.Workplaces, "IdWorkplace", "Name", monitor.IdWorkplace);
            return View(monitor);
        }

        // POST: Monitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMonitor,Name,InventNumber,IdWorkplace")] Monitor monitor)
        {
            if (id != monitor.IdMonitor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonitorExists(monitor.IdMonitor))
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
            ViewData["IdWorkplace"] = new SelectList(_context.Workplaces, "IdWorkplace", "Name", monitor.IdWorkplace);
            return View(monitor);
        }

        // GET: Monitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitor = await _context.Monitors
                .Include(m => m.Workplace)
                .FirstOrDefaultAsync(m => m.IdMonitor == id);
            if (monitor == null)
            {
                return NotFound();
            }

            return View(monitor);
        }

        // POST: Monitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monitor = await _context.Monitors.FindAsync(id);
            if (monitor != null)
            {
                _context.Monitors.Remove(monitor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonitorExists(int id)
        {
            return _context.Monitors.Any(e => e.IdMonitor == id);
        }
    }
}
