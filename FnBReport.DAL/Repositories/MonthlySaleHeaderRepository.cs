using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using System.Data;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FnBReport.DAL.Repositories
{
    public class MonthlySaleHeaderRepository : EfBaseRepository<MonthlySaleHeader, int>, IMonthlySaleHeaderRepository
    {
        public MonthlySaleHeaderRepository(POSReportContext context) : base(context)
        {
        }

        public async Task<MonthlySaleHeader?> GetMonthlySaleWithDetailsAsync(string storeCode, int year, int month)
        {
            return await _dbSet
                .Include(h => h.MonthlySales)
                .FirstOrDefaultAsync(h => h.StoreCode == storeCode && h.Year == year && h.Month == month && !h.IsDeleted);
        }

        public async Task<MonthlySaleHeader?> GetMonthlySaleWithDetailsByIdAsync(int id)
        {
            return await _dbSet
                .Include(h => h.MonthlySales)
                .FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);
        }

        public async Task SaveWithDetailsAsync(MonthlySaleHeader header)
        {
            if (header.Id == 0)
            {
                await _dbSet.AddAsync(header);
            }
            else
            {
                _dbSet.Update(header);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWithDetailsAsync(MonthlySaleHeader header)
        {
            _context.Remove(header);
            await _context.SaveChangesAsync();
        }
    }
}
