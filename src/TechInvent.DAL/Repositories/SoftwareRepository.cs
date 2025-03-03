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
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly TechInventContext _context;

        public SoftwareRepository(TechInventContext context)
        {
            _context = context;
        }

        public async Task<Software?> AddAsync(Software tclass)
        {
            _context.Softwares.Add(tclass);
            await _context.SaveChangesAsync();
            return tclass;
        }

        public async Task DeleteAsync(int id)
        {
            var software = await _context.Softwares.FindAsync(id);
            if (software == null) return;

            _context.Softwares.Remove(software);
            await _context.SaveChangesAsync();
        }

        public async Task<Software?> FindAsync(Expression<Func<Software, bool>> predicate)
        {
            return await _context.Softwares.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Software>> GetAllAsync()
        {
            return await _context.Softwares.AsNoTracking().ToListAsync();
        }

        public async Task<Software?> GetByIdAsync(int id)
        {
            return await _context.Softwares.FindAsync(id);
        }

        public async Task<Software> GetOrCreate(string name, string version, Manufacturer manufacturer)
        {
            var software = _context.Softwares.FirstOrDefault(s => s.Name == name && s.Version == version && s.IdManufacturer == s.IdManufacturer);
            if (software == null)
            {
                software = new Software { Name = name, Version = version, IdManufacturer = manufacturer.IdManufacturer };
                _context.Softwares.Add(software);
                await _context.SaveChangesAsync();
            }
            return software;
        }

        public async Task<Software?> UpdateAsync(int id, Software tclass)
        {
            var existingSoftware = await _context.Softwares.FindAsync(id);
            if (existingSoftware == null) return null;

            _context.Entry(existingSoftware).CurrentValues.SetValues(tclass);
            await _context.SaveChangesAsync();
            return existingSoftware;
        }
    }

}
