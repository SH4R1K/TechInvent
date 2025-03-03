using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Interfaces
{
    public interface ISoftwareService
    {
        Task<List<InstalledSoftware>> CheckSoftwareAsync(List<InstalledSoftware> software, Workplace workplace);
    }
}
