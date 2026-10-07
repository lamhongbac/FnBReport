using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;

namespace FnBReport.DAL.Repositories
{
    public class StoreRepository : EfBaseRepository<Store, int>, IStoreRepository
    {
        public StoreRepository(POSReportContext context) : base(context)
        {
        }
    }

    public class StoreGroupRepository : EfBaseRepository<StoreGroup, int>, IStoreGroupRepository
    {
        public StoreGroupRepository(POSReportContext context) : base(context)
        {
        }
    }
}
