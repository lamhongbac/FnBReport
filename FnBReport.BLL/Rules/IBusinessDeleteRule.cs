using FnBReport.Shared.Models;
using System.Threading.Tasks;

namespace FnBReport.BLL.Rules
{
    public interface IBusinessDeleteRule<in TId>
    {
        bool IsApplicable(TId id);
        Task<BOProcessResult> ValidateDeleteAsync(TId id);
    }
}
