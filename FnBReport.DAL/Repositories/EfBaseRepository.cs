using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FnBReport.DAL.Repositories
{
    public class EfBaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class, IBaseEntity<TId>
    {
        protected readonly POSReportContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EfBaseRepository(POSReportContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted);
        }

        public virtual async Task<TEntity?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<string>(e, "Number") == code && !e.IsDeleted);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.IsDeleted = false;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            entity.ModifiedOn = DateTime.Now;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteSoftAsync(TId id, string updatedBy)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.ModifiedBy = updatedBy;
                entity.ModifiedOn = DateTime.Now;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<bool> ExistsAsync(TId id)
        {
            return await _dbSet.AnyAsync(e => e.Id!.Equals(id) && !e.IsDeleted);
        }
    }
}
