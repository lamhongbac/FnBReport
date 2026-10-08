using FnBReport.BLL.Services;
using FnBReport.DAL.Models;
using FnBReport.GUI.ViewModels;
using FnBReport.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnBReport.GUI.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductService _productService;

        public ProductAppService(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<ProductGroupViewModel>> GetAllProductGroupsAsync()
        {
            var groups = await _productService.GetAllProductGroupsAsync();
            return groups.Select(x => new ProductGroupViewModel
            {
                Id = x.Id,
                Number = x.Number,
                Name = x.Name,
                Category = x.Category
            });
        }

        public async Task<BOProcessResult> SaveProductGroupAsync(ProductGroupViewModel vm, string savedBy)
        {
            try
            {
                var entity = new ProductGroup
                {
                    Id = vm.Id,
                    Number = vm.Number,
                    Name = vm.Name,
                    Category = vm.Category
                };

                if (vm.Id == 0)
                {
                    var result = await _productService.CreateProductGroupAsync(entity, savedBy);
                    if (result.IsSuccess)
                    {
                        vm.Id = entity.Id; // Gán ID gốc về View Model để Quick Add auto-select
                    }
                    return result;
                }
                else
                {
                    return await _productService.UpdateProductGroupAsync(entity, savedBy);
                }
            }
            catch (Exception ex)
            {
                // Yêu cầu: Bắt và show nguyên bản lỗi DB
                return BOProcessResult.Failed(ex.Message);
            }
        }

        public async Task<BOProcessResult> DeleteProductGroupAsync(int id, string deletedBy)
        {
            try
            {
                return await _productService.DeleteProductGroupAsync(id, deletedBy);
            }
            catch (Exception ex)
            {
                return BOProcessResult.Failed(ex.Message);
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Number = x.Number,
                Name = x.Name,
                GroupCode = x.GroupCode,
                Unit = x.Unit,
                Cosprice = x.Cosprice,
                SellingPrice = x.SellingPrice
            });
        }

        public async Task<BOProcessResult> SaveProductAsync(ProductViewModel vm, string savedBy)
        {
            try
            {
                var entity = new Product
                {
                    Id = vm.Id,
                    Number = vm.Number,
                    Name = vm.Name,
                    GroupCode = vm.GroupCode,
                    Unit = vm.Unit,
                    Cosprice = vm.Cosprice,
                    SellingPrice = vm.SellingPrice
                };

                if (vm.Id == 0)
                {
                    return await _productService.CreateProductAsync(entity, savedBy);
                }
                else
                {
                    return await _productService.UpdateProductAsync(entity, savedBy);
                }
            }
            catch (Exception ex)
            {
                // Yêu cầu: Bắt và show nguyên bản lỗi DB
                return BOProcessResult.Failed(ex.Message);
            }
        }

        public async Task<BOProcessResult> DeleteProductAsync(int id, string deletedBy)
        {
            try
            {
                return await _productService.DeleteProductAsync(id, deletedBy);
            }
            catch (Exception ex)
            {
                return BOProcessResult.Failed(ex.Message);
            }
        }
    }
}
