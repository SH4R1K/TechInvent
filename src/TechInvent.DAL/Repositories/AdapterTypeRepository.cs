using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechInvent.DAL.Data;
using TechInvent.DM.Models;

namespace TechInvent.DAL.Interfaces
{
    public class AdapterTypeRepository : IAdapterTypeRepository
    {
        private readonly TechInventContext _context;
        public AdapterTypeRepository(TechInventContext context)
        {
            _context = context;
        }

        public async Task<AdapterType?> AddAsync(AdapterType tclass)
        {
            await _context.AdapterTypes.AddAsync(tclass);
            await _context.SaveChangesAsync();
            return tclass;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AdapterTypes.FindAsync(id);
            if (entity != null)
            {
                _context.AdapterTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AdapterType?> FindAsync(Expression<Func<AdapterType, bool>> predicate)
        {
            return await _context.AdapterTypes.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<AdapterType>> GetAllAsync()
        {
            return await _context.AdapterTypes.ToListAsync();
        }

        public async Task<AdapterType?> GetByIdAsync(int id)
        {
            return await _context.AdapterTypes.FindAsync(id);
        }

        public async Task<AdapterType> GetOrCreateAsync(string name)
        {

            var adapterType = _context.AdapterTypes.FirstOrDefault(m => m.Name == name);
            if (adapterType == null)
            {
                adapterType = new AdapterType { Name = name };
                await _context.AdapterTypes.AddAsync(adapterType);
                await _context.SaveChangesAsync();
            }
            return adapterType;
        }

        public async Task<AdapterType?> UpdateAsync(int id, AdapterType tclass)
        {
            var existingEntity = await _context.AdapterTypes.FindAsync(id);
            if (existingEntity == null)
                return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(tclass);
            await _context.SaveChangesAsync();
            return existingEntity;
        }
    }
}
