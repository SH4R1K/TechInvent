using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechInvent.DM.Models;

namespace TechInvent.DAL.Interfaces
{
    public interface ICabinetRepository: IRepository<Cabinet>
    {
        public Task<Workplace?> AddWorkplaceAsync(int id, Workplace workplace);
    }
}
