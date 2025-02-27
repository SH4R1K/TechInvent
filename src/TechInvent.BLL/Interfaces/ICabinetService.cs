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
        Task<CabinetDto?> GetByIdAsync(int id);
        Task<CabinetDto?> CreateAsync(CabinetDto cabinetDto);
        Task<CabinetDto?> UpdateAsync(int id, CabinetDto cabinetDto);
        Task DeleteAsync(int id);
    }
}
