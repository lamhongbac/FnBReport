using FnBReport.DAL.Models;
using FnBReport.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FnBReport.BLL.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreGroup>> GetAllStoreGroupsAsync();
        Task<BOProcessResult> CreateStoreGroupAsync(StoreGroup group, string createdBy);
        Task<BOProcessResult> UpdateStoreGroupAsync(StoreGroup group, string updatedBy);
        Task<BOProcessResult> DeleteStoreGroupAsync(int id, string deletedBy);

        Task<IEnumerable<Store>> GetAllStoresAsync();
        Task<Store?> GetStoreByIdAsync(int id);
        Task<BOProcessResult> CreateStoreAsync(Store store, string createdBy);
        Task<BOProcessResult> UpdateStoreAsync(Store store, string updatedBy);
        Task<BOProcessResult> DeleteStoreAsync(int id, string deletedBy);
    }
}
