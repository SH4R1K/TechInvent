using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>?> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> CreateAsync(T tclass);
        Task<T?> UpdateAsync(int id, T tclass);
        Task DeleteAsync(int id);
    }
}
