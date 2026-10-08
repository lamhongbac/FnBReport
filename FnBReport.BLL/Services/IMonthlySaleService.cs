using FnBReport.Shared.Models;
using FnBReport.DAL.Models;
using System.Threading.Tasks;
using System;

namespace FnBReport.BLL.Services
{
    public interface IMonthlySaleService
    {
        Task<MonthlySaleHeader?> GetMonthlySaleAsync(string storeCode, int year, int month);
        Task<BOProcessResult> SaveMonthlySaleAsync(MonthlySaleHeader header, string user);
        Task<BOProcessResult> DeleteMonthlySaleAsync(int headerId, string user);
    }
}
