using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.BLL.Dto;

namespace TechInvent.BLL.Interfaces
{
    public interface ICabinetService
    {
        Task<List<CabinetDto>?> GetAllAsync();
        Task<InventCabinetDto?> GetByIdAsync(int id);
        Task<InventCabinetDto?> AddAsync(InventCabinetDto cabinetDto);
        Task<InventCabinetDto?> UpdateAsync(int id, InventCabinetDto cabinetDto);
        Task DeleteAsync(int id);
    }
}
