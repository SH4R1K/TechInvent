using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechInvent.DAL.Data;
using TechInvent.DAL.Interfaces;
using TechInvent.DM.Models;

namespace TechInvent.DAL.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly TechInventContext _context;

        public ManufacturerRepository(TechInventContext context)
        {
            _context = context;
        }

        public async Task<Manufacturer?> AddAsync(Manufacturer manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
            return manufacturer;
        }

        public async Task DeleteAsync(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
        }

        public async Task<Manufacturer?> FindAsync(Expression<Func<Manufacturer, bool>> predicate)
        {
            return await _context.Manufacturers.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Manufacturer>> GetAllAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer?> GetByIdAsync(int id)
        {
            return await _context.Manufacturers.FindAsync(id);
        }

        public async Task<Manufacturer> GetOrCreateAsync(string name)
        {
            var manufacturer = _context.Manufacturers.FirstOrDefault(m => m.Name == name);
            if (manufacturer == null)
            {
                manufacturer = new Manufacturer { Name = name };
                await _context.Manufacturers.AddAsync(manufacturer);
                await _context.SaveChangesAsync();
            }
            return manufacturer;
        }

        public async Task<Manufacturer?> UpdateAsync(int id, Manufacturer manufacturer)
        {
            var existingManufacturer = await _context.Manufacturers.FindAsync(id);
            if (existingManufacturer == null) return null;

            _context.Entry(existingManufacturer).CurrentValues.SetValues(manufacturer);
            await _context.SaveChangesAsync();
            return existingManufacturer;
        }
    }
}
