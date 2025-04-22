using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace WebMVC.Controllers
{
    public class TechRequestsController : Controller
    {
        private readonly TechInventContext _context;

        public TechRequestsController(TechInventContext context)
        {
            _context = context;
        }

        // GET: TechRequests
        public async Task<IActionResult> Index()
        {
            var techInventContext = _context.TechRequests.Include(t => t.Cabinet).Include(t => t.RequestType);
            return View(await techInventContext.ToListAsync());
        }

        // GET: TechRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var techRequest = await _context.TechRequests
                .Include(t => t.Cabinet)
                .Include(t => t.RequestType)
                .FirstOrDefaultAsync(m => m.IdRequest == id);
            if (techRequest == null)
            {
                return NotFound();
            }

            return View(techRequest);
        }

        // GET: TechRequests/Create
        public IActionResult Create()
        {
            ViewData["IdCabinet"] = new SelectList(_context.Cabinets, "IdCabinet", "IdCabinet");
            ViewData["IdRequestType"] = new SelectList(_context.RequestTypes, "IdRequestType", "Name");
            return View();
        }

        // POST: TechRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRequest,IdRequestType,IdCabinet,Title,Description,IsActive,CreationDate")] TechRequest techRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(techRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCabinet"] = new SelectList(_context.Cabinets, "IdCabinet", "IdCabinet", techRequest.IdCabinet);
            ViewData["IdRequestType"] = new SelectList(_context.RequestTypes, "IdRequestType", "Name", techRequest.IdRequestType);
            return View(techRequest);
        }

        // GET: TechRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var techRequest = await _context.TechRequests.FindAsync(id);
            if (techRequest == null)
            {
                return NotFound();
            }
            ViewData["IdCabinet"] = new SelectList(_context.Cabinets, "IdCabinet", "IdCabinet", techRequest.IdCabinet);
            ViewData["IdRequestType"] = new SelectList(_context.RequestTypes, "IdRequestType", "Name", techRequest.IdRequestType);
            return View(techRequest);
        }

        // POST: TechRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRequest,IdRequestType,IdCabinet,Title,Description,IsActive,CreationDate")] TechRequest techRequest)
        {
            if (id != techRequest.IdRequest)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(techRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechRequestExists(techRequest.IdRequest))
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
            ViewData["IdCabinet"] = new SelectList(_context.Cabinets, "IdCabinet", "IdCabinet", techRequest.IdCabinet);
            ViewData["IdRequestType"] = new SelectList(_context.RequestTypes, "IdRequestType", "Name", techRequest.IdRequestType);
            return View(techRequest);
        }

        // GET: TechRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var techRequest = await _context.TechRequests
                .Include(t => t.Cabinet)
                .Include(t => t.RequestType)
                .FirstOrDefaultAsync(m => m.IdRequest == id);
            if (techRequest == null)
            {
                return NotFound();
            }

            return View(techRequest);
        }

        // POST: TechRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var techRequest = await _context.TechRequests.FindAsync(id);
            if (techRequest != null)
            {
                _context.TechRequests.Remove(techRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechRequestExists(int id)
        {
            return _context.TechRequests.Any(e => e.IdRequest == id);
        }
    }
}
