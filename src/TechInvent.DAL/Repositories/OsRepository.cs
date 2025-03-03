using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace TechInvent.DAL.Interfaces
{
    public class OsRepository : IOsRepository
    {
        private readonly TechInventContext _context;

        public OsRepository(TechInventContext context)
        {
            _context = context;
        }

        public async Task<Os?> AddAsync(Os tclass)
        {
            _context.Os.Add(tclass);
            await _context.SaveChangesAsync();
            return tclass;
        }

        public async Task DeleteAsync(int id)
        {
            var os = await _context.Os.FindAsync(id);
            _context.Os.Remove(os);
            await _context.SaveChangesAsync();
        }

        public async Task<Os?> FindAsync(Expression<Func<Os, bool>> predicate)
        {
            return await _context.Os.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Os>> GetAllAsync()
        {
            return await _context.Os.AsNoTracking().ToListAsync();
        }

        public async Task<Os?> GetByIdAsync(int id)
        {
            return await _context.Os.FindAsync(id);
        }

        public async Task<Os> GetOrCreateAsync(string name, string osVersion)
        {
            var os = _context.Os.FirstOrDefault(s => s.OsName == name && s.OsVersion == osVersion);
            if (os == null)
            {
                os = new Os { OsName = name, OsVersion = osVersion };
                _context.Os.Add(os);
                await _context.SaveChangesAsync();
            }
            return os;
        }

        public async Task<Os?> UpdateAsync(int id, Os tclass)
        {
            var existingOs = await _context.Os.FindAsync(id);
            if (existingOs == null) return null;

            _context.Entry(existingOs).CurrentValues.SetValues(tclass);
            await _context.SaveChangesAsync();
            return existingOs;
        }
    }
}
