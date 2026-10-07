using FnBReport.Shared.Models;
using System.Threading.Tasks;

namespace FnBReport.BLL.Rules
{
    public interface IBusinessSaveRule<in T> where T : class
    {
        bool IsApplicable(T entity);
        Task<BOProcessResult> ValidateAsync(T entity);
    }
}
