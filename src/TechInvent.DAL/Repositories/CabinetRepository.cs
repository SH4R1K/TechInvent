using TechInvent.DAL.Interfaces;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TechInvent.DAL.Repositories
{
    public class CabinetRepository : ICabinetRepository
    {
        private readonly TechInventContext _context;
        public CabinetRepository(TechInventContext context)
        {
            _context = context;
        }

        public async Task<Workplace?> AddWorkplaceAsync(int id, Workplace workplace)
        {
            var cabinet = await _context.Cabinets.Include(c => c.Workplaces).FirstOrDefaultAsync(c => c.IdCabinet == id);
            cabinet.Workplaces.Add(workplace);
            _context.Update(cabinet);
            await _context.SaveChangesAsync();
            return workplace;
        }

        public async Task<Cabinet?> AddAsync(Cabinet cabinet)
        {
            _context.Cabinets.Add(cabinet);
            await _context.SaveChangesAsync();
            return cabinet;
        }

        public async Task DeleteAsync(int id)
        {
            var cabinet = await _context.Cabinets.FindAsync(id);
            _context.Cabinets.Remove(cabinet);
            await _context.SaveChangesAsync();
        }

        public async Task<Cabinet?> FindAsync(Expression<Func<Cabinet, bool>> predicate)
        {
            return await _context.Cabinets.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Cabinet>?> GetAllAsync()
        {
            return await _context.Cabinets.Include(c => c.Workplaces).ThenInclude(c => c.Os).ToListAsync();
        }

        public async Task<Cabinet?> GetByIdAsync(int id)
        {
            return await _context.Cabinets.FindAsync(id);
        }

        public async Task<Cabinet?> UpdateAsync(int id, Cabinet cabinet)
        {
            var existingCabinet = await _context.Cabinets.FindAsync(id);
            if (existingCabinet == null) return null;

            _context.Entry(existingCabinet).CurrentValues.SetValues(cabinet);
            await _context.SaveChangesAsync();
            return existingCabinet;
        }
    }
}