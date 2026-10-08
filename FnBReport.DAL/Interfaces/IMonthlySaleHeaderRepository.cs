using FnBReport.DAL.Models;

using System.Threading.Tasks;

namespace FnBReport.DAL.Interfaces
{
    public interface IMonthlySaleHeaderRepository : IBaseRepository<MonthlySaleHeader, int>
    {
        Task<MonthlySaleHeader?> GetMonthlySaleWithDetailsAsync(string storeCode, int year, int month);
        Task<MonthlySaleHeader?> GetMonthlySaleWithDetailsByIdAsync(int id);
        Task SaveWithDetailsAsync(MonthlySaleHeader header);
        Task DeleteWithDetailsAsync(MonthlySaleHeader header);
    }
}
