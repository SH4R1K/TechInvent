using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<Workplace?> CreateAsync(Workplace workplace)
        {
            return workplace;
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Workplace>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Workplace?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Workplace?> UpdateAsync(int id, Workplace workplace)
        {
            throw new NotImplementedException();
        }
    }
}
