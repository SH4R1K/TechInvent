using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechInvent.BLL.Dto;

namespace TechInvent.BLL.Interfaces
{
    public interface IWorkplaceService
    {
        Task<List<WorkplaceDto>?> GetAllAsync();
        Task<WorkplaceDto?> GetByIdAsync(int id);
        Task<WorkplaceDto?> AddAsync(WorkplaceDto workplaceDto);
        Task<WorkplaceDto?> UpdateAsync(int id, WorkplaceDto workplaceDto);
        Task DeleteAsync(int id);
    }
}
