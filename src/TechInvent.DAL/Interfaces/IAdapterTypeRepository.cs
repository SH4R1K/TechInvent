using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.DM.Models;

namespace TechInvent.DAL.Interfaces
{
    public interface IAdapterTypeRepository : IRepository<AdapterType>
    {
        public Task<AdapterType> GetOrCreateAsync(string name);
    }
}
