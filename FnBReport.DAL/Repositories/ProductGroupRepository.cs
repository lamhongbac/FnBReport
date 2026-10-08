using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;

namespace FnBReport.DAL.Repositories
{
    public class ProductGroupRepository : EfBaseRepository<ProductGroup, int>, IProductGroupRepository
    {
        public ProductGroupRepository(POSReportContext context) : base(context)
        {
        }
    }
}
