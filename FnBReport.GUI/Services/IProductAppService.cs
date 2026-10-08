using FnBReport.GUI.ViewModels;
using FnBReport.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FnBReport.GUI.Services
{
    public interface IProductAppService
    {
        Task<IEnumerable<ProductGroupViewModel>> GetAllProductGroupsAsync();
        Task<BOProcessResult> SaveProductGroupAsync(ProductGroupViewModel vm, string savedBy);
        Task<BOProcessResult> DeleteProductGroupAsync(int id, string deletedBy);

        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<BOProcessResult> SaveProductAsync(ProductViewModel vm, string savedBy);
        Task<BOProcessResult> DeleteProductAsync(int id, string deletedBy);
    }
}
