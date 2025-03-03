using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.DM.Models;

namespace TechInvent.BLL.Interfaces
{
    public interface IComponentService
    {
        Task<List<Component>> CheckComponentsAsync(List<Component> components);
    }
}
