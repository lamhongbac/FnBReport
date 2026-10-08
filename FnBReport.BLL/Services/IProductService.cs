using FnBReport.DAL.Models;
using FnBReport.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FnBReport.BLL.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductGroup>> GetAllProductGroupsAsync();
        Task<BOProcessResult> CreateProductGroupAsync(ProductGroup group, string createdBy);
        Task<BOProcessResult> UpdateProductGroupAsync(ProductGroup group, string updatedBy);
        Task<BOProcessResult> DeleteProductGroupAsync(int id, string deletedBy);

        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<BOProcessResult> CreateProductAsync(Product product, string createdBy);
        Task<BOProcessResult> UpdateProductAsync(Product product, string updatedBy);
        Task<BOProcessResult> DeleteProductAsync(int id, string deletedBy);
    }
}
