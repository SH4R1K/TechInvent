using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TechInvent.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>?> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> AddAsync(T tclass);
        Task<T?> UpdateAsync(int id, T tclass);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(int id);
    }
}
