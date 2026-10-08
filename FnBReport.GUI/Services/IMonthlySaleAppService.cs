using FnBReport.GUI.ViewModels;
using FnBReport.Shared.Models;
using System.Threading.Tasks;

namespace FnBReport.GUI.Services
{
    public interface IMonthlySaleAppService
    {
        Task<MonthlySaleHeaderViewModel> GetOrInitMonthlySaleAsync(string storeCode, int year, int month);
        Task<BOProcessResult> SaveMonthlySaleAsync(MonthlySaleHeaderViewModel model, string user);
    }
}
