using FnBReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FnBReport.DAL.Interfaces
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class, IBaseEntity<TId>
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task<TEntity?> GetByCodeAsync(string code);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteSoftAsync(TId id, string updatedBy);
        
        Task<bool> ExistsAsync(TId id);
    }
}
