using FnBReport.Shared.Models;
using FnBReport.BLL.Services;
using FnBReport.DAL.Models;
using FnBReport.GUI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnBReport.GUI.Services
{
    public class MonthlySaleAppService : IMonthlySaleAppService
    {
        private readonly IMonthlySaleService _monthlySaleService;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;

        public MonthlySaleAppService(IMonthlySaleService monthlySaleService, IStoreService storeService, IProductService productService)
        {
            _monthlySaleService = monthlySaleService;
            _storeService = storeService;
            _productService = productService;
        }

        public async Task<MonthlySaleHeaderViewModel> GetOrInitMonthlySaleAsync(string storeCode, int year, int month)
        {
            var header = await _monthlySaleService.GetMonthlySaleAsync(storeCode, year, month);
            var stores = await _storeService.GetAllStoresAsync();
            var store = stores.FirstOrDefault(s => s.Number == storeCode);
            
            if (header != null)
            {
                // Load existing
                var vm = new MonthlySaleHeaderViewModel
                {
                    Id = header.Id,
                    Year = header.Year ?? year,
                    Month = header.Month,
                    StoreCode = header.StoreCode,
                    StoreName = store?.Name ?? "",
                    Tables = header.Tables,
                    Tc = header.Tc,
                    Sales = header.Sales,
                    Cos = header.Cos,
                    TotalMargin = header.TotalMargin,
                    MinMenuSelling = store?.MinMenuSelling,
                    MinSalesContribution = store?.MinSalesContribution,
                    Details = header.MonthlySales.Select(d => new MonthlySaleViewModel
                    {
                        Id = d.Id,
                        ProductCode = d.ProductCode,
                        ProductName = d.ProductName,
                        Unit = d.Unit,
                        GroupCode = d.GroupCode,
                        Cosprice = d.Cosprice,
                        SellingPrice = d.SellingPrice,
                        Margin = d.Margin,
                        SoldQuantity = d.SoldQuantity,
                        CosPercentage = d.CosPercentage,
                        SaleMixPercentage = d.SaleMixPercentage,
                        MarginMixPercentage = d.MarginMixPercentage,
                        ProductMixPercentage = d.ProductMixPercentage,
                        TcTable = d.TcTable,
                        Per100Tc = d.Per100Tc,
                        Per100Quality = d.Per100Quality,
                        Per100Selling = d.Per100Selling,
                        Popularity = d.Popularity,
                        Profitability = d.Profitability,
                        ItemClass = d.ItemClass
                    }).ToList()
                };
                return vm;
            }
            else
            {
                // Init new draft
                var vm = new MonthlySaleHeaderViewModel
                {
                    Id = 0,
                    Year = year,
                    Month = month,
                    StoreCode = storeCode,
                    StoreName = store?.Name ?? "",
                    Tables = (int?)(store?.NumOfTables ?? 0),
                    MinMenuSelling = store?.MinMenuSelling,
                    MinSalesContribution = store?.MinSalesContribution,
                    Tc = 0,
                    Sales = 0,
                    Cos = 0,
                    TotalMargin = 0,
                    Details = new List<MonthlySaleViewModel>()
                };

                var products = await _productService.GetAllProductsAsync();
                foreach (var p in products)
                {
                    vm.Details.Add(new MonthlySaleViewModel
                    {
                        ProductCode = p.Number,
                        ProductName = p.Name,
                        Unit = p.Unit,
                        GroupCode = p.GroupCode,
                        Cosprice = p.Cosprice ?? 0,
                        SellingPrice = p.SellingPrice ?? 0,
                        Margin = (p.SellingPrice ?? 0) - (p.Cosprice ?? 0),
                        SoldQuantity = 0
                    });
                }
                return vm;
            }
        }

        public async Task<BOProcessResult> SaveMonthlySaleAsync(MonthlySaleHeaderViewModel model, string user)
        {
            var entity = new MonthlySaleHeader
            {
                Id = model.Id,
                Year = model.Year,
                Month = model.Month,
                StoreCode = model.StoreCode,
                Tables = model.Tables,
                Tc = model.Tc,
                Sales = model.Sales,
                Cos = model.Cos,
                TotalMargin = model.TotalMargin,
                MonthlySales = model.Details.Select(d => new MonthlySale
                {
                    Id = d.Id,
                    ProductCode = d.ProductCode,
                    ProductName = d.ProductName,
                    Unit = d.Unit,
                    GroupCode = d.GroupCode,
                    Cosprice = d.Cosprice,
                    SellingPrice = d.SellingPrice,
                    Margin = d.Margin,
                    SoldQuantity = d.SoldQuantity
                    // Note: Other calc fields will be handled in separate calculation task
                }).ToList()
            };

            return await _monthlySaleService.SaveMonthlySaleAsync(entity, user);
        }
    }
}
