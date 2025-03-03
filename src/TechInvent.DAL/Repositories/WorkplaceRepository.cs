using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechInvent.DAL.Data;
using TechInvent.DAL.Interfaces;
using TechInvent.DM.Models;

namespace TechInvent.DAL.Repositories
{
    public class WorkplaceRepository : IWorkplaceRepository
    {
        private readonly TechInventContext _context;

        public WorkplaceRepository(TechInventContext context)
        {
            _context = context;
        }

        public async Task<Workplace?> AddAsync(Workplace workplace)
        {
            _context.Workplaces.Add(workplace);
            await _context.SaveChangesAsync();
            return workplace;
        }

        public async Task DeleteAsync(int id)
        {
            var workplace = await _context.Workplaces.FindAsync(id);
            _context.Workplaces.Remove(workplace);
            await _context.SaveChangesAsync();
        }

        public async Task<Workplace?> FindAsync(Expression<Func<Workplace, bool>> predicate)
        {
            return await _context.Workplaces.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Workplace>> GetAllAsync()
        {
            return await _context.Workplaces.AsNoTracking().ToListAsync();
        }

        public async Task<Workplace?> GetByIdAsync(int id)
        {
            return await _context.Workplaces.FindAsync(id);
        }

        public async Task<Workplace?> UpdateAsync(int id, Workplace workplace)
        {
            var existingWorkplace = await _context.Workplaces
        .Include(w => w.InstalledSoftware)
        .Include(w => w.Components)
        .FirstOrDefaultAsync(w => w.IdWorkplace == id);

            if (existingWorkplace == null) return null;

            existingWorkplace.LastUpdate = DateTime.UtcNow;
            existingWorkplace.Os = workplace.Os;
            existingWorkplace.IdCabinet = workplace.IdCabinet;

            existingWorkplace.Components = workplace.Components;

            var softwareToRemove = existingWorkplace.InstalledSoftware
                .Where(installedSoftware => !workplace.InstalledSoftware.Any(newSoftware => newSoftware.IdSoftware == installedSoftware.IdSoftware))
                .ToList();

            foreach (var software in softwareToRemove)
            {
                existingWorkplace.InstalledSoftware.Remove(software);
            }

            var softwareToAdd = workplace.InstalledSoftware
                .Where(newSoftware => !existingWorkplace.InstalledSoftware.Any(installedSoftware => installedSoftware.IdSoftware == newSoftware.IdSoftware))
                .Select(newSoftware => new InstalledSoftware
                {
                    Workplace = existingWorkplace,
                    IdSoftware = newSoftware.IdSoftware,
                    Software = newSoftware.Software
                })
                .ToList();

            existingWorkplace.InstalledSoftware.AddRange(softwareToAdd);

            await _context.SaveChangesAsync();

            return existingWorkplace;
        }

    }
}
