using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.BLL.Dto;

namespace TechInvent.BLL.Interfaces
{
    public interface IInventService
    {
        Task<InventCabinetDto> InventWorkplace(InventCabinetDto cabinetDto);
    }
}
