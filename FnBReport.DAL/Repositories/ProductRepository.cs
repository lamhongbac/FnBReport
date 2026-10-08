using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnBReport.DAL.Repositories
{
    public class ProductRepository : EfBaseRepository<Product, int>, IProductRepository
    {
        public ProductRepository(POSReportContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbSet.Where(x => !x.IsDeleted)
                               .Include(x => x.GroupCodeNavigation)
                               .ToListAsync();
        }
    }
}
