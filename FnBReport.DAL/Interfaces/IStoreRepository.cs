using FnBReport.DAL.Models;

namespace FnBReport.DAL.Interfaces
{
    public interface IStoreRepository : IBaseRepository<Store, int>
    {
    }

    public interface IStoreGroupRepository : IBaseRepository<StoreGroup, int>
    {
    }
}
