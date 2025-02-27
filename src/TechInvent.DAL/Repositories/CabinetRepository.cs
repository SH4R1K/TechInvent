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

        public async Task<Cabinet?> CreateAsync(Cabinet cabinet)
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
            return await _context.Cabinets.FindAsync(predicate);
        }

        public async Task<List<Cabinet>?> GetAllAsync()
        {
            return await _context.Cabinets.ToListAsync();
        }

        public async Task<Cabinet?> GetByIdAsync(int id)
        {
            return await _context.Cabinets.FindAsync(id);
        }

        public async Task<Cabinet?> UpdateAsync(int id, Cabinet cabinet)
        {
            _context.Cabinets.Update(cabinet);
            await _context.SaveChangesAsync();
            return cabinet;
        }
    }
}
