using FnBReport.Shared.Models;
using FnBReport.DAL.Interfaces;
using FnBReport.DAL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FnBReport.BLL.Services
{
    public class MonthlySaleService : IMonthlySaleService
    {
        private readonly IMonthlySaleHeaderRepository _headerRepo;

        public MonthlySaleService(IMonthlySaleHeaderRepository headerRepo)
        {
            _headerRepo = headerRepo;
        }

        public async Task<MonthlySaleHeader?> GetMonthlySaleAsync(string storeCode, int year, int month)
        {
            return await _headerRepo.GetMonthlySaleWithDetailsAsync(storeCode, year, month);
        }

        public async Task<BOProcessResult> SaveMonthlySaleAsync(MonthlySaleHeader header, string user)
        {
            try
            {
                if (header.Id == 0)
                {
                    // Create
                    header.CreatedBy = user;
                    header.CreatedOn = DateTime.Now;
                    header.ModifiedBy = user;
                    header.ModifiedOn = DateTime.Now;

                    foreach (var detail in header.MonthlySales)
                    {
                        detail.Id = Guid.NewGuid();
                        detail.CreatedBy = user;
                        detail.CreatedOn = DateTime.Now;
                        detail.ModifiedBy = user;
                        detail.ModifiedOn = DateTime.Now;
                    }
                    await _headerRepo.SaveWithDetailsAsync(header);
                }
                else
                {
                    // Update
                    var existingHeader = await _headerRepo.GetMonthlySaleWithDetailsByIdAsync(header.Id);

                    if (existingHeader == null)
                        return BOProcessResult.Failed("Bản ghi không tồn tại");

                    existingHeader.Tc = header.Tc;
                    existingHeader.Sales = header.Sales;
                    existingHeader.Cos = header.Cos;
                    existingHeader.TotalMargin = header.TotalMargin;
                    existingHeader.ModifiedBy = user;
                    existingHeader.ModifiedOn = DateTime.Now;

                    // Update details
                    foreach (var newDetail in header.MonthlySales)
                    {
                        var existingDetail = existingHeader.MonthlySales.FirstOrDefault(d => d.ProductCode == newDetail.ProductCode);
                        if (existingDetail != null)
                        {
                            existingDetail.SoldQuantity = newDetail.SoldQuantity;
                            existingDetail.SellingPrice = newDetail.SellingPrice;
                            existingDetail.Cosprice = newDetail.Cosprice;
                            existingDetail.Margin = newDetail.Margin;
                            existingDetail.ModifiedBy = user;
                            existingDetail.ModifiedOn = DateTime.Now;
                        }
                        else
                        {
                            newDetail.Id = Guid.NewGuid();
                            newDetail.CreatedBy = user;
                            newDetail.CreatedOn = DateTime.Now;
                            newDetail.ModifiedBy = user;
                            newDetail.ModifiedOn = DateTime.Now;
                            existingHeader.MonthlySales.Add(newDetail);
                        }
                    }

                    await _headerRepo.SaveWithDetailsAsync(existingHeader);
                }

                return new BOProcessResult { OK = true, Result = header };
            }
            catch (Exception ex)
            {
                return BOProcessResult.Failed(ex.Message);
            }
        }

        public async Task<BOProcessResult> DeleteMonthlySaleAsync(int headerId, string user)
        {
            try
            {
                var header = await _headerRepo.GetMonthlySaleWithDetailsByIdAsync(headerId);
                if (header == null) return BOProcessResult.Failed("Không tìm thấy dữ liệu");

                await _headerRepo.DeleteWithDetailsAsync(header);
                return BOProcessResult.Success();
            }
            catch (Exception ex)
            {
                return BOProcessResult.Failed(ex.Message);
            }
        }
    }
}
