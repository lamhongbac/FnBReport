using FnBReport.BLL.Constants;
using FnBReport.DAL.Models;
using FnBReport.Shared.Models;
using System.Threading.Tasks;

namespace FnBReport.BLL.Rules.StoreRules
{
    public class StoreNameRequiredRule : IBusinessSaveRule<Store>
    {
        public bool IsApplicable(Store entity) => true;

        public Task<BOProcessResult> ValidateAsync(Store entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                return Task.FromResult(BOProcessResult.Failed(DomainErrorCodes.Store.NameEmpty));
            return Task.FromResult(BOProcessResult.Success());
        }
    }
}
