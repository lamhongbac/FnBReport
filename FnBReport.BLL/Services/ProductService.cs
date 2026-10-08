using FnBReport.BLL.Constants;
using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using FnBReport.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FnBReport.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductGroupRepository _groupRepo;

        public ProductService(IProductRepository productRepo, IProductGroupRepository groupRepo)
        {
            _productRepo = productRepo;
            _groupRepo = groupRepo;
        }

        public async Task<IEnumerable<ProductGroup>> GetAllProductGroupsAsync() => await _groupRepo.GetAllAsync();

        public async Task<BOProcessResult> CreateProductGroupAsync(ProductGroup group, string createdBy)
        {
            group.CreatedBy = createdBy;
            await _groupRepo.AddAsync(group);
            return BOProcessResult.Success();
        }

        public async Task<BOProcessResult> UpdateProductGroupAsync(ProductGroup group, string updatedBy)
        {
            var existing = await _groupRepo.GetByIdAsync(group.Id);
            if (existing == null) return BOProcessResult.Failed("Nhóm sản phẩm không tồn tại.");

            existing.Name = group.Name;
            existing.Number = group.Number;
            existing.Category = group.Category;
            existing.ModifiedBy = updatedBy;

            await _groupRepo.UpdateAsync(existing);
            return BOProcessResult.Success();
        }

        public async Task<BOProcessResult> DeleteProductGroupAsync(int id, string deletedBy)
        {
            await _groupRepo.DeleteSoftAsync(id, deletedBy);
            return BOProcessResult.Success();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _productRepo.GetAllAsync();

        public async Task<Product?> GetProductByIdAsync(int id) => await _productRepo.GetByIdAsync(id);

        public async Task<BOProcessResult> CreateProductAsync(Product product, string createdBy)
        {
            product.CreatedBy = createdBy;
            await _productRepo.AddAsync(product);
            return BOProcessResult.Success();
        }

        public async Task<BOProcessResult> UpdateProductAsync(Product product, string updatedBy)
        {
            var existing = await _productRepo.GetByIdAsync(product.Id);
            if (existing == null) return BOProcessResult.Failed("Sản phẩm không tồn tại.");

            existing.Name = product.Name;
            existing.Number = product.Number;
            existing.GroupCode = product.GroupCode;
            existing.Unit = product.Unit;
            existing.Cosprice = product.Cosprice;
            existing.SellingPrice = product.SellingPrice;
            existing.ModifiedBy = updatedBy;

            await _productRepo.UpdateAsync(existing);
            return BOProcessResult.Success();
        }

        public async Task<BOProcessResult> DeleteProductAsync(int id, string deletedBy)
        {
            await _productRepo.DeleteSoftAsync(id, deletedBy);
            return BOProcessResult.Success();
        }
    }
}
